using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLogic.DTOs.EmployeeDTOs;
using DataAccess.Models.EmployeeModels;

namespace BussinessLogic.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            //To map the Properities that doesnt match names

                     //TSource     TDestination
            CreateMap<Employee, GetEmployeeDto>()
                //TDestination        TSource     
                .ForMember(dest=>dest.EmpType , options=>options.MapFrom(emp=>emp.EmployeeType))
                .ForMember(dest=>dest.EmpGender , options=>options.MapFrom(emp=>emp.Gender))
                ; //Get
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest=>dest.EmpType , options => options.MapFrom(emp=>emp.EmployeeType))
                .ForMember(dest=>dest.EmpGender , options => options.MapFrom(emp=>emp.Gender))
                .ForMember(dest=>dest.HiringDate , options => options.MapFrom(emp=>DateOnly.FromDateTime(emp.HiringDate)))

                ; //Get

                           //TSource     TDestination
            CreateMap<CreateEmployeeDto  , Employee > ()
                .ForMember(dest=> dest.HiringDate , options=> options.MapFrom(empDto=> empDto.HiringDate.ToDateTime(TimeOnly.MinValue)))
                ; //Create
             
            CreateMap<UpdateEmployeeDto  , Employee > ()
                .ForMember(dest=> dest.HiringDate , options=> options.MapFrom(empDto=> empDto.HiringDate.ToDateTime(TimeOnly.MinValue)))
                ; //Update
        }
         

    }
}
