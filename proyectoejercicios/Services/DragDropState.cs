using System;

namespace proyectoejercicios.Services;

public class DragDropState
{
    public event Action? OnDragStateChanged;

    public int? DraggedEjercicioId { get; private set; }
    public Guid? DraggedItemId { get; private set; }
    public bool IsDraggingNewLabel { get; private set; }
    public string? DraggedLabelText { get; private set; }
    public string? SourceDiaId { get; private set; }
    public int? SourceIndex { get; private set; }
    public string? DraggedTitle { get; private set; }
    public bool IsTapPlacementMode { get; private set; }

    public bool IsDragging => DraggedEjercicioId.HasValue || DraggedItemId.HasValue || IsDraggingNewLabel;

    public void StartDragExercise(int id, string? title = null, bool tapMode = false)
    {
        DraggedEjercicioId = id;
        DraggedItemId = null;
        IsDraggingNewLabel = false;
        DraggedLabelText = null;
        SourceDiaId = null;
        SourceIndex = null;
        DraggedTitle = title;
        IsTapPlacementMode = tapMode;
        NotifyStateChanged();
    }

    public void StartDragItem(Guid id, string? sourceDiaId = null, int? sourceIndex = null, string? title = null, bool tapMode = false)
    {
        DraggedItemId = id;
        DraggedEjercicioId = null;
        IsDraggingNewLabel = false;
        DraggedLabelText = null;
        SourceDiaId = sourceDiaId;
        SourceIndex = sourceIndex;
        DraggedTitle = title;
        IsTapPlacementMode = tapMode;
        NotifyStateChanged();
    }

    public void StartDragNewLabel(string defaultText = "Nota / Indicación", bool tapMode = false)
    {
        IsDraggingNewLabel = true;
        DraggedLabelText = string.IsNullOrWhiteSpace(defaultText) ? "Nota / Indicación" : defaultText;
        DraggedEjercicioId = null;
        DraggedItemId = null;
        SourceDiaId = null;
        SourceIndex = null;
        DraggedTitle = $"🏷️ {DraggedLabelText}";
        IsTapPlacementMode = tapMode;
        NotifyStateChanged();
    }

    public void Reset()
    {
        if (!IsDragging && SourceDiaId == null && SourceIndex == null && !IsTapPlacementMode)
            return;

        DraggedEjercicioId = null;
        DraggedItemId = null;
        IsDraggingNewLabel = false;
        DraggedLabelText = null;
        SourceDiaId = null;
        SourceIndex = null;
        DraggedTitle = null;
        IsTapPlacementMode = false;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnDragStateChanged?.Invoke();
}
