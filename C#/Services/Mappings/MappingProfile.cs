namespace Services.Mappings
{
    using AutoMapper;
    using Database;
    using MyCS.InputModels;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ManualInputModel, Account>();
            this.CreateMap<ManualInputModel, Client>();
            this.CreateMap<ManualInputModel, Loan>();
        }
    }
}
