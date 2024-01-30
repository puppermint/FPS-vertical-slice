public interface IWeapon
{
    public void HandleLeftClick();
    public void HandleRightClick();
    public int HandleReload(int reserveAmmo);
    public bool GetIsReloading();
    public int GetBullets();
}
