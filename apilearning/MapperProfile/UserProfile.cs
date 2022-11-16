using AutoMapper;
using apilearning.Models;
using apilearning.ModelDtos;

namespace apilearning.MapperProfile {
    /// <summary>
    /// 使用AutoMapper 做对象映射   模型类->模型DTO类(面向API消费者的类)
    /// </summary>
    public class UserProfile : Profile {
        public UserProfile() {
            CreateMap<User, UserDto>()
                .ForMember(target => target.BornDate,// user.BornDate => userDto.BornDate
                member => member.MapFrom(src => src.BornDate.ToShortDateString())
                )
                .ForMember(target => target.Id,
                member => member.MapFrom(src => src.Id)
                )
                 .ForMember(target => target.Name,
                member => member.MapFrom(src => src.Name)
                )
                  .ForMember(target => target.Salary,
                member => member.MapFrom(src => src.Salary)
                );
        }
    }
}
