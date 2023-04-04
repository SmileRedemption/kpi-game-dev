using Core.Enums;
using Hero;
using UnityEngine;
namespace Core.Input
{
    public class ExternalDevicesInputReader : IEntityInputSource
    {
        public Vector2 Direction => new Vector2(UnityEngine.Input.GetAxisRaw(InputConstants.Horizontal.ToString()), UnityEngine.Input.GetAxisRaw(InputConstants.Vertical.ToString()));
        public bool Jump { get; private set; }
        
        public void ResetOneTimeAction() => Jump = false;

        public void OnUpdate()
        {
            if (UnityEngine.Input.GetButtonDown(InputConstants.Jump.ToString()))
            {
                Jump = true;
            }
        }
    }
}