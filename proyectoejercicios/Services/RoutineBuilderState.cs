using System;
using System.Collections.Generic;
using System.Linq;
using proyectoejercicios.Models;

namespace proyectoejercicios.Services;

public class RoutineBuilderState
{
    public event Action? OnStateChanged;

    public List<ItemRutina> RoutineItems { get; private set; } = new();
    public List<DiaRutina> Dias { get; private set; } = new();

    public RoutineBuilderState()
    {
        // Default 7 days
        string[] defaultDays = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
        foreach (var d in defaultDays)
        {
            Dias.Add(new DiaRutina { Nombre = d });
        }
    }

    public void AddDay()
    {
        Dias.Add(new DiaRutina { Nombre = $"Día {Dias.Count + 1}" });
        NotifyStateChanged();
    }

    public void RemoveDay(string diaId)
    {
        var dia = Dias.FirstOrDefault(d => d.Id == diaId);
        if (dia != null)
        {
            Dias.Remove(dia);
            RoutineItems.RemoveAll(i => i.DiaId == diaId);
            NotifyStateChanged();
        }
    }

    public void RenameDay(string diaId, string newName)
    {
        var dia = Dias.FirstOrDefault(d => d.Id == diaId);
        if (dia != null)
        {
            dia.Nombre = newName;
            NotifyStateChanged();
        }
    }

    public void AddExerciseToDay(Ejercicio ejercicio, string diaId, int index = -1)
    {
        var itemsInDay = RoutineItems.Where(i => i.DiaId == diaId).OrderBy(i => i.OrdenVisual).ToList();
        
        var newItem = new ItemRutina
        {
            Id = Guid.NewGuid(),
            EjercicioId = ejercicio.Id,
            Ejercicio = ejercicio,
            DiaId = diaId,
            Series = 3,
            Repeticiones = 10
        };

        if (index < 0 || index >= itemsInDay.Count)
        {
            itemsInDay.Add(newItem);
        }
        else
        {
            itemsInDay.Insert(index, newItem);
        }

        RoutineItems.Add(newItem);
        UpdateOrders(itemsInDay);
        NotifyStateChanged();
    }

    public void AddLabelToDay(string texto, string diaId, int index = -1)
    {
        var itemsInDay = RoutineItems.Where(i => i.DiaId == diaId).OrderBy(i => i.OrdenVisual).ToList();
        
        var newItem = new ItemRutina
        {
            Id = Guid.NewGuid(),
            DiaId = diaId,
            EsEtiqueta = true,
            TextoEtiqueta = string.IsNullOrWhiteSpace(texto) ? "Nota / Indicación" : texto
        };

        if (index < 0 || index >= itemsInDay.Count)
        {
            itemsInDay.Add(newItem);
        }
        else
        {
            itemsInDay.Insert(index, newItem);
        }

        RoutineItems.Add(newItem);
        UpdateOrders(itemsInDay);
        NotifyStateChanged();
    }

    public void DuplicateItem(Guid itemId, string? targetDiaId = null, int? targetIndex = null)
    {
        var sourceItem = RoutineItems.FirstOrDefault(i => i.Id == itemId);
        if (sourceItem == null) return;

        string destDiaId = string.IsNullOrWhiteSpace(targetDiaId) ? sourceItem.DiaId : targetDiaId;
        var itemsInDestDay = RoutineItems
            .Where(i => i.DiaId == destDiaId)
            .OrderBy(i => i.OrdenVisual)
            .ToList();

        int insertIndex;
        if (targetIndex.HasValue)
        {
            insertIndex = targetIndex.Value;
        }
        else if (destDiaId == sourceItem.DiaId)
        {
            int currentIdx = itemsInDestDay.FindIndex(i => i.Id == itemId);
            insertIndex = currentIdx >= 0 ? currentIdx + 1 : -1;
        }
        else
        {
            insertIndex = -1;
        }

        var duplicatedItem = new ItemRutina
        {
            Id = Guid.NewGuid(),
            DiaId = destDiaId,
            EsEtiqueta = sourceItem.EsEtiqueta,
            TextoEtiqueta = sourceItem.TextoEtiqueta,
            EjercicioId = sourceItem.EjercicioId,
            Ejercicio = sourceItem.Ejercicio,
            Series = sourceItem.Series,
            Repeticiones = sourceItem.Repeticiones
        };

        if (insertIndex < 0 || insertIndex >= itemsInDestDay.Count)
        {
            itemsInDestDay.Add(duplicatedItem);
        }
        else
        {
            itemsInDestDay.Insert(insertIndex, duplicatedItem);
        }

        RoutineItems.Add(duplicatedItem);
        UpdateOrders(itemsInDestDay);
        NotifyStateChanged();
    }

    public void MoveItemToSlot(Guid itemId, string targetDiaId, int slotIndex)
    {
        var item = RoutineItems.FirstOrDefault(i => i.Id == itemId);
        if (item == null) return;

        if (slotIndex < 0)
        {
            MoveItem(itemId, targetDiaId, -1);
            return;
        }

        if (item.DiaId == targetDiaId)
        {
            var currentItems = RoutineItems
                .Where(i => i.DiaId == targetDiaId)
                .OrderBy(i => i.OrdenVisual)
                .ToList();

            int oldIndex = currentItems.FindIndex(i => i.Id == itemId);
            if (oldIndex != -1)
            {
                if (slotIndex == oldIndex || slotIndex == oldIndex + 1)
                {
                    NotifyStateChanged();
                    return;
                }

                int adjustedIndex = slotIndex > oldIndex ? slotIndex - 1 : slotIndex;
                MoveItem(itemId, targetDiaId, adjustedIndex);
                return;
            }
        }

        MoveItem(itemId, targetDiaId, slotIndex);
    }

    public void MoveItem(Guid itemId, string targetDiaId, int newIndex)
    {
        var item = RoutineItems.FirstOrDefault(i => i.Id == itemId);
        if (item == null) return;

        var sourceDiaId = item.DiaId;
        item.DiaId = targetDiaId;

        var itemsInTargetDay = RoutineItems
            .Where(i => i.DiaId == targetDiaId && i.Id != itemId)
            .OrderBy(i => i.OrdenVisual)
            .ToList();
        
        if (newIndex < 0 || newIndex >= itemsInTargetDay.Count)
        {
            itemsInTargetDay.Add(item);
        }
        else
        {
            itemsInTargetDay.Insert(newIndex, item);
        }

        UpdateOrders(itemsInTargetDay);

        if (sourceDiaId != targetDiaId)
        {
            var itemsInSourceDay = RoutineItems
                .Where(i => i.DiaId == sourceDiaId)
                .OrderBy(i => i.OrdenVisual)
                .ToList();
            UpdateOrders(itemsInSourceDay);
        }

        NotifyStateChanged();
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

        MoveItem(itemId, item.DiaId, targetIndex);
    }

    private void UpdateOrders(List<ItemRutina> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].OrdenVisual = i;
        }
    }

    public void RemoveItem(Guid itemId)
    {
        var item = RoutineItems.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            RoutineItems.Remove(item);
            var itemsInDay = RoutineItems.Where(i => i.DiaId == item.DiaId).OrderBy(i => i.OrdenVisual).ToList();
            UpdateOrders(itemsInDay);
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}
