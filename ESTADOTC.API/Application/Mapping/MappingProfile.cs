using AutoMapper;
using ESTADOTC.API.Application.DTOs;
using ESTADOTC.API.Domain.Entities;

namespace ESTADOTC.API.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Card, CardStatementDto>();
        CreateMap<Transaction, TransactionDto>();
        CreateMap<CardFinancialSummary, CardFinancialSummaryDto>();
        CreateMap<MonthlyPurchaseTotals, MonthlyPurchaseTotalsDto>();
    }
}