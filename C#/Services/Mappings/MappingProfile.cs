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
            this.CreateMap<ManualInputModel, Client>()
                .ForMember(x => x.Application_Month,
                    opt => opt.MapFrom(x => x.Application_Date.Month));
            this.CreateMap<ManualInputModel, Loan>();
        }
    }
}
