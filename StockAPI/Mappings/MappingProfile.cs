using AutoMapper;
using StockAPI.Models;
using StockAPI.DTOs;

namespace StockAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Stock, StockWithoutTradeDTO>(); // map from Stock to StockWithoutTradeDTO
            CreateMap<UserTrade, UserTradeDTO>();
            CreateMap<UserTradeForCreationDTO, UserTrade>(); //to add new trade
            CreateMap<UserTradeForUpdateDTO, UserTrade>(); //to update new trade (closing a trade will have a TradeCloseDate)
            // CreateMap<UserTradeForCreationDTO, UserTradeDTO>(); //to add new trade
        }
    }
}