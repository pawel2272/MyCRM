using AutoMapper;
using MyCrm.Domain.Command.Contact;
using MyCrm.Domain.Command.Order;
using MyCrm.Domain.Command.Role;
using MyCrm.Domain.Command.Todo;
using MyCrm.Domain.Command.User;
using MyCrm.Domain.Entities;
using MyCrm.Domain.Query.Dto;

namespace MyCrm.Domain
{
    public class EntityMappingProfile : Profile
    {
        public void CreateMapForContact()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();

            CreateMap<Contact, AddContactCommand>();
            CreateMap<AddContactCommand, Contact>();

            CreateMap<Contact, EditContactCommand>();
            CreateMap<EditContactCommand, Contact>();
        }

        public void CreateMapForOrder()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<Order, AddOrderCommand>();
            CreateMap<AddOrderCommand, Order>();

            CreateMap<Order, EditOrderCommand>();
            CreateMap<EditOrderCommand, Order>();
        }

        public void CreateMapForRole()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<Role, AddRoleCommand>();
            CreateMap<AddRoleCommand, Role>();

            CreateMap<Role, EditRoleCommand>();
            CreateMap<EditRoleCommand, Role>();
        }

        public void CreateMapForTodo()
        {
            CreateMap<Todo, TodoDto>();
            CreateMap<TodoDto, Todo>();

            CreateMap<Todo, AddTodoCommand>();
            CreateMap<AddTodoCommand, Todo>();

            CreateMap<Todo, EditTodoCommand>();
            CreateMap<EditTodoCommand, Todo>();
        }

        public void CreateMapForUser()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<User, AddUserCommand>();
            CreateMap<AddUserCommand, User>();

            CreateMap<User, EditUserCommand>();
            CreateMap<EditUserCommand, User>();
        }

        public EntityMappingProfile()
        {
            CreateMapForContact();
            CreateMapForOrder();
            CreateMapForRole();
            CreateMapForTodo();
            CreateMapForUser();
        }
    }
}
