public interface IUpgradeable
{
    int Level { get; }
    bool CanUpgrade(int playerMoney);
    int GetUpgradeCost();
    void Upgrade();
}
