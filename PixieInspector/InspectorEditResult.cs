using PixieInspector.Diagnostics;

namespace PixieInspector;

// One attempt per member; diagnostics are captured before later reads can clear them.
public sealed record InspectorEditOutcome(InspectorNode Node, bool Pending,
    InspectorFailureEventArgs? Failure, Exception? Exception)
{
    public bool Accepted => Failure == null && Exception == null;
}

public sealed class InspectorEditResult
{
    internal InspectorEditResult(IEnumerable<InspectorEditOutcome> outcomes) => Outcomes = Array.AsReadOnly(outcomes.ToArray());
    public IReadOnlyList<InspectorEditOutcome> Outcomes { get; }
    public bool Accepted => Outcomes.All(o => o.Accepted);
    public bool Pending => Outcomes.Any(o => o.Pending);
    // Normal setters are not transactional; consult outcomes/native readback after failure.
    public bool MayHavePartialEffects => !Accepted;
}
