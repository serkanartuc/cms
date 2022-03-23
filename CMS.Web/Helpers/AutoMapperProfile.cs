using AutoMapper;
using CMS.Web.Areas.admin.Models;
using CMS.Web.Data;

namespace CMS.Web.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<User, UserEditModel>();
            CreateMap<UserEditModel, User>()
                    .ForMember(q => q.Password, option => option.Condition((src, dst, srcMember) => !string.IsNullOrWhiteSpace(srcMember)));
                    //.BeforeMap((src, dst) => {
                    //    if (!string.IsNullOrWhiteSpace(src.Password))
                    //     {
                    //        dst.Password = src.Password;
                    //     }
                    // });

            CreateMap<Language, LanguageModel>();
            CreateMap<LanguageModel, Language>();

            CreateMap<Slider, SliderModel>();
            CreateMap<SliderModel, Slider>();
        }
    }
}
