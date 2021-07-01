using AutoMapper;
using Database;
using MyCS.InputModels;

namespace Services.Mappings
{
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
