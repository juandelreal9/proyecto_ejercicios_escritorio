using System;
using System.Collections.Generic;
using System.Linq;
using proyectoejerciciosescritorio.Models;

namespace proyectoejerciciosescritorio.Services;

public class RoutineBuilderState
{
    public event Action? OnStateChanged;

    public List<ItemRutina> RoutineItems { get; private set; } = new();
    public List<DiaRutina> Dias { get; private set; } = new();
    public string ActiveDiaId { get; private set; } = string.Empty;
    public bool IsRoutineStarted => Dias.Count > 0;

    public DiaRutina? ActiveDia => Dias.FirstOrDefault(d => d.Id == ActiveDiaId) ?? Dias.LastOrDefault();

    public DiaRutina StartRoutine()
    {
        if (Dias.Count == 0)
        {
            var dia1 = new DiaRutina { Nombre = "Día 1" };
            Dias.Add(dia1);
            ActiveDiaId = dia1.Id;
        }
        else if (string.IsNullOrWhiteSpace(ActiveDiaId) || !Dias.Any(d => d.Id == ActiveDiaId))
        {
            ActiveDiaId = Dias[0].Id;
        }

        NotifyStateChanged();
        return ActiveDia!;
    }

    public DiaRutina ResetAndStartNewRoutine()
    {
        RoutineItems.Clear();
        Dias.Clear();
        var dia1 = new DiaRutina { Nombre = "Día 1" };
        Dias.Add(dia1);
        ActiveDiaId = dia1.Id;
        NotifyStateChanged();
        return dia1;
    }

    public DiaRutina AdvanceOrCreateNextDay()
    {
        if (Dias.Count == 0)
        {
            return StartRoutine();
        }

        int currentIdx = Dias.FindIndex(d => d.Id == ActiveDiaId);
        if (currentIdx >= 0 && currentIdx < Dias.Count - 1)
        {
            ActiveDiaId = Dias[currentIdx + 1].Id;
        }
        else
        {
            var nuevoDia = new DiaRutina { Nombre = $"Día {Dias.Count + 1}" };
            Dias.Add(nuevoDia);
            ActiveDiaId = nuevoDia.Id;
        }

        NotifyStateChanged();
        return ActiveDia!;
    }

    public void SelectDay(string diaId)
    {
        if (Dias.Any(d => d.Id == diaId))
        {
            ActiveDiaId = diaId;
            NotifyStateChanged();
        }
    }

    public void RenameDay(string diaId, string newName)
    {
        var dia = Dias.FirstOrDefault(d => d.Id == diaId);
        if (dia != null && !string.IsNullOrWhiteSpace(newName))
        {
            dia.Nombre = newName.Trim();
            NotifyStateChanged();
        }
    }

    public void RemoveDay(string diaId)
    {
        var dia = Dias.FirstOrDefault(d => d.Id == diaId);
        if (dia != null)
        {
            Dias.Remove(dia);
            RoutineItems.RemoveAll(i => i.DiaId == diaId);
            if (ActiveDiaId == diaId)
            {
                ActiveDiaId = Dias.LastOrDefault()?.Id ?? string.Empty;
            }
            NotifyStateChanged();
        }
    }

    public ItemRutina AddExerciseToDay(Ejercicio ejercicio, string diaId, int series = 4, int repeticiones = 12)
    {
        var itemsInDay = RoutineItems.Where(i => i.DiaId == diaId).OrderBy(i => i.OrdenVisual).ToList();

        var newItem = new ItemRutina
        {
            Id = Guid.NewGuid(),
            EjercicioId = ejercicio.Id,
            Ejercicio = ejercicio,
            DiaId = diaId,
            Series = series > 0 ? series : 4,
            Repeticiones = repeticiones > 0 ? repeticiones : 12,
            OrdenVisual = itemsInDay.Count
        };

        RoutineItems.Add(newItem);
        NotifyStateChanged();
        return newItem;
    }

    public ItemRutina AddLabelToDay(string texto, string diaId)
    {
        var itemsInDay = RoutineItems.Where(i => i.DiaId == diaId).OrderBy(i => i.OrdenVisual).ToList();

        var newItem = new ItemRutina
        {
            Id = Guid.NewGuid(),
            DiaId = diaId,
            EsEtiqueta = true,
            TextoEtiqueta = string.IsNullOrWhiteSpace(texto) ? "Nota / Indicación" : texto.Trim(),
            OrdenVisual = itemsInDay.Count
        };

        RoutineItems.Add(newItem);
        NotifyStateChanged();
        return newItem;
    }

    public void UpdateExerciseSetsReps(Guid itemId, int series, int repeticiones)
    {
        var item = RoutineItems.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            item.Series = Math.Max(1, series);
            item.Repeticiones = Math.Max(1, repeticiones);
            NotifyStateChanged();
        }
    }

    public void MoveItemStep(Guid itemId, int step)
    {
        var item = RoutineItems.FirstOrDefault(i => i.Id == itemId);
        if (item == null) return;

        var itemsInDay = RoutineItems
            .Where(i => i.DiaId == item.DiaId)
            .OrderBy(i => i.OrdenVisual)
            .ToList();

        int currentIndex = itemsInDay.FindIndex(i => i.Id == itemId);
        if (currentIndex == -1) return;

        int targetIndex = currentIndex + step;
        if (targetIndex < 0 || targetIndex >= itemsInDay.Count) return;

        itemsInDay.RemoveAt(currentIndex);
        itemsInDay.Insert(targetIndex, item);

        for (int i = 0; i < itemsInDay.Count; i++)
        {
            itemsInDay[i].OrdenVisual = i;
        }

        NotifyStateChanged();
    }

    public void RemoveItem(Guid itemId)
    {
        var item = RoutineItems.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            string diaId = item.DiaId;
            RoutineItems.Remove(item);
            var itemsInDay = RoutineItems.Where(i => i.DiaId == diaId).OrderBy(i => i.OrdenVisual).ToList();
            for (int i = 0; i < itemsInDay.Count; i++)
            {
                itemsInDay[i].OrdenVisual = i;
            }
            NotifyStateChanged();
        }
    }

    public void NotifyChanged() => NotifyStateChanged();

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}

