using R3;
using TMPro;

namespace Game.UI
{
	public static class ObservableUIUtils
	{
		public static Observable<string> OnValueChangedAsObservable(this TMP_InputField inputField)
		{
			return Observable.Create<string, TMP_InputField>(inputField, static (observer, i) =>
			{
				observer.OnNext(i.text);
				return i.onValueChanged.AsObservable(i.destroyCancellationToken).Subscribe(observer);
			});

			// return inputField.onValueChanged.AsObservable(inputField.destroyCancellationToken);
		}
	}
}