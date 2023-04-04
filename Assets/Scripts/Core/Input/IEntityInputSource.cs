using UnityEngine;

namespace Core.Input
{
    public interface IEntityInputSource
    {
        Vector2 Direction { get; }
        bool Jump { get; }

        void ResetOneTimeAction();
    }
}