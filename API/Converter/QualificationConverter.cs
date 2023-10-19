using AutoMapper;
using API.Models;
using API.DTOs.Qualification;

namespace API.Converter
{
    public class QualificationConverter: Profile
    {
        public QualificationConverter()
        {
            //consultar
            CreateMap<Qualification, QualificationDTO>()
                .ForMember(dto => dto.Id, options => options.MapFrom(model => model.QualificationsId));

            CreateMap<QualificationDTO, Qualification>()
               .ForMember(model => model.QualificationsId, options => options.MapFrom(dto => dto.Id));

            //Guardar
            CreateMap<QualificationSaveDTO, Qualification>().ReverseMap();
        }
    }
}
