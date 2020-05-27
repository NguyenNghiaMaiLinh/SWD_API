using AutoMapper;
using DemoApp.Core.Data.Enity;
using DemoApp.Core.ViewModel;
using DemoApp.Core.ViewModel.ViewPage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DemoApp.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DateTime, string>().ConvertUsing(new DatetimeToStringConverter());
            CreateMap<string, DateTime>().ConvertUsing(new StringToDatetimeConverter());

            CreateMap<TaskViewPage, Task>().ReverseMap();
            CreateMap<Task, TaskViewPage>().ReverseMap();

            CreateMap<AccountViewPage, Account>().ReverseMap();
            CreateMap<Account, AccountViewPage>().ReverseMap();

            CreateMap<ProductViewPage, Product>().ReverseMap();
            CreateMap<Product, ProductViewPage>().ReverseMap();

            CreateMap<DateTime, string>().ConvertUsing(new DatetimeToStringConverter());
            CreateMap<string, DateTime>().ConvertUsing(new StringToDatetimeConverter());
        }

        public class DatetimeToStringConverter : ITypeConverter<DateTime, string>
        {
            public string Convert(DateTime source, string destination, ResolutionContext context)
            {
                return source.ToString("dd/MM/yyyy");
            }
        }
        public class StringToDatetimeConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(string source, DateTime destination, ResolutionContext context)
            {
                return DateTime.ParseExact(source, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
    }
}
