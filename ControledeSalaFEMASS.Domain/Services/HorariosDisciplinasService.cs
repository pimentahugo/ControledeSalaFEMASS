using ControledeSalaFEMASS.Domain.Enums;

namespace ControledeSalaFEMASS.Domain.Services;
public static class HorariosDisciplinasService
{
    private static readonly Dictionary<int, List<(DayOfWeek Dia, TempoSala Tempo)>> _mapeamento = new Dictionary<int, List<(DayOfWeek, TempoSala)>>
    {
        {1, new List<(DayOfWeek, TempoSala)> {(DayOfWeek.Monday, TempoSala.Tempo1), (DayOfWeek.Tuesday, TempoSala.Tempo2)}},
        {2, new List<(DayOfWeek, TempoSala)> {(DayOfWeek.Monday, TempoSala.Tempo2), (DayOfWeek.Tuesday, TempoSala.Tempo1)}},
        {3, new List<(DayOfWeek, TempoSala)> {(DayOfWeek.Tuesday, TempoSala.Tempo3), (DayOfWeek.Wednesday, TempoSala.Tempo3)}},
        {4, new List<(DayOfWeek, TempoSala)> {(DayOfWeek.Wednesday, TempoSala.Tempo1), (DayOfWeek.Thursday, TempoSala.Tempo2)}},
        {5, new List<(DayOfWeek, TempoSala)> {(DayOfWeek.Thursday, TempoSala.Tempo1), (DayOfWeek.Friday, TempoSala.Tempo2)}},
        {6, new List<(DayOfWeek, TempoSala)> {(DayOfWeek.Thursday, TempoSala.Tempo3), (DayOfWeek.Friday, TempoSala.Tempo1)}}
    };

    public static List<(DayOfWeek Dia, TempoSala Tempo)> ObterHorariosDisciplina(int ordemDisciplina)
    {
        if (_mapeamento.TryGetValue(ordemDisciplina, out var horarios))
        {
            return horarios;
        }

        throw new ArgumentException("Ordem da disciplina inválido", nameof(ordemDisciplina));
    }
}
