using Beatmaps;

namespace Game.Visuals
{
	public interface IActiveSpinnerFactory
	{
		void CreateActiveSpinner(Spinner spinner);
		void RemoveCurrent();
	}
}