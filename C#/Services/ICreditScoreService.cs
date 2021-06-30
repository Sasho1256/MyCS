namespace Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using MyCS.InputModels;

    public interface ICreditScoreService
    {
        public Task<Dictionary<Account, List<string>>> CreateRecordFromManualInput(ManualInputModel input);
        public void CalculateScore(Account result);
    }
}
