namespace VFX
{
	public interface IVFXFactory
	{
		string GetVFXType();
		IVFX Spawn();
	}
}