namespace Services
{
    using System.Threading.Tasks;
    using Database;
    using MyCS.InputModels;

    public interface ICreditScoreService
    {
        public Task<Account> CreateRecordFromManualInput(ManualInputModel input);
    }
}
