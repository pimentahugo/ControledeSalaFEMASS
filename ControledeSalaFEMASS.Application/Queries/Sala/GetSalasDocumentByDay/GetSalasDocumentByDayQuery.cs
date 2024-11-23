using MediatR;

namespace ControledeSalaFEMASS.Application.Queries.Sala.GetSalasDocumentByDay;
public record GetSalasDocumentByDayQuery(DayOfWeek DayOfWeek) : IRequest<byte[]>
{

}