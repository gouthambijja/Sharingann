using AutoMapper;
using ExpressTrackerDBAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerLogicLayer.Services
{
    internal class AutoMappers
    {
        public static Mapper InitializeCategoryAutoMapper()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.BLCategory, Category>()
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            });
           return new Mapper(config);
           
        }
        public static Mapper InitializeTransactionAutoMapper()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.BLTransaction, Transaction>()
                    .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            });
             return new Mapper(config);
        }
        public static Mapper InitializeUserAutoMapper()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.BLUser, User>()
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username)); 
                    
            });
             return new Mapper(config);
        }
    }
}
