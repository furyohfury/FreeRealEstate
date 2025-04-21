using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using Zenject;

namespace Game
{
	public sealed class CellViewListPresenter : IInitializable, IDisposable
	{
		private readonly CellViewList _cellViewList;

		private readonly ActiveBundleService _activeBundleService;
		private readonly CellViewFactory _cellViewFactory;
		private readonly CellPresenterFactory _cellPresenterFactory;
		private readonly List<CellPresenter> _cellPresenters = new();

		private readonly CompositeDisposable _disposable = new();

		public CellViewListPresenter(
			ActiveBundleService activeBundleService,
			CellViewList cellViewList,
			CellViewFactory cellViewFactory,
			CellPresenterFactory cellPresenterFactory)
		{
			_activeBundleService = activeBundleService;
			_cellViewList = cellViewList;
			_cellViewFactory = cellViewFactory;
			_cellPresenterFactory = cellPresenterFactory;
		}

		public void Initialize()
		{
			_activeBundleService.ActiveCellBundle
			                    .Subscribe(OnBundleChanged)
			                    .AddTo(_disposable);
		}

		private void OnBundleChanged(CellBundle bundle)
		{
			Clear();
			var views = new CellView[bundle.Cells.Length];

			for (int i = 0, count = bundle.Cells.Length; i < count; i++)
			{
				var cell = bundle.Cells[i];
				var view = _cellViewFactory.CreateCellView();
				view.SetSprite(cell.Icon);
				views[i] = view;
				var presenter = _cellPresenterFactory.CreatePresenter(view, cell);
				_cellPresenters.Add(presenter);
			}

			_cellViewList.SetCellViews(views, bundle.Rows, bundle.Columns);
		}

		private void Clear()
		{
			for (int i = 0, count = _cellPresenters.Count; i < count; i++)
			{
				_cellPresenters[i].Dispose();
			}

			_cellViewList.DestroyViews();
			_cellPresenters.Clear();
		}

		public void Dispose()
		{
			_disposable.Dispose();
			for (int i = 0, count = _cellPresenters.Count; i < count; i++)
			{
				_cellPresenters[i].Dispose();
			}
		}
	}
}