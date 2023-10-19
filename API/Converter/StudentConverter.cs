using AutoMapper;
using API.Models;
using System.Globalization;
using API.DTOs.Student;

namespace API.Converter
{
    public class StudentConverter: Profile
    {
        public StudentConverter()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(dto => dto.Id, options => options.MapFrom(model => model.StudentId));

            CreateMap<StudentDTO, Student>()
               .ForMember(model => model.StudentId, options => options.MapFrom(dto => dto.Id));

            //Guardar de dto a model
            CreateMap<StudentSaveDTO, Student>().ReverseMap();

            //Actualizar de dto a model
            CreateMap<StudentUpdateDTO, Student>().ReverseMap();


            //Covertir modelo de studiantes con las cualificaiones a dto
            CreateMap<Student, StudentsQualificationsDTO>()
                 .ForMember(dto => dto.Id, options => options.MapFrom(model => model.StudentId));

            CreateMap<StudentsQualificationsDTO, Student>()
               .ForMember(model => model.StudentId, options => options.MapFrom(dto => dto.Id));

        }
    }
}
