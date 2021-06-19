namespace Services
{
    using System.Threading.Tasks;
    using MyCS.InputModels;

    public interface ICreditScoreService
    {
        public Task CreateRecordFromManualInput(ManualInputModel input);
    }
}
