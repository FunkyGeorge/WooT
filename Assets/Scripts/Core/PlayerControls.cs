// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Core/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""e1fc7fb6-084d-43f7-88f1-b7e2e5cf0263"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8d01d9b7-1d0d-4f40-9043-3e12e6de3d72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""82703a95-ec70-4b35-bdf3-cfc50987e6a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""5fc5da79-c8d7-47d7-9b17-fba73f47c6d3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""35d37326-2d96-4c24-9a4a-f67d0c622002"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Feedback"",
                    ""type"": ""Button"",
                    ""id"": ""9d632134-5136-47ea-8645-d0274f69a5f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left Stick"",
                    ""type"": ""Button"",
                    ""id"": ""bed9f1f4-3334-48ee-a58c-b1ee578d1d60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button"",
                    ""type"": ""Button"",
                    ""id"": ""edb7cae1-27d5-4022-b509-519524579182"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Key"",
                    ""type"": ""Button"",
                    ""id"": ""acbe6842-0f60-4326-92c8-8aea8d2ff025"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""83e99efc-0d6e-4a62-8abc-f0df9b5df118"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b64690d9-7af8-4876-af6a-0fd31a6e65f4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0516802-df24-4602-8141-2947c6bf53f1"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""899119cf-04d9-440f-ba78-f70b1ed93cf6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD keys"",
                    ""id"": ""58aff446-383d-4e72-9a07-1aa838234d26"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c5987d80-8da1-409f-bf52-c5ae83f0eb4e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a5f96b63-158f-424f-a456-6897a7a13593"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fb58b746-6824-4f67-985d-92c0add042f3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6664909d-a62b-486d-83c8-4b866c6c42b9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5b5ffd8e-0b42-42d4-b6d8-49630363b3b0"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""015e009d-79e6-4802-92a0-01693c2fa268"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bf42190-9b51-4578-a6bd-76c7efcd55a5"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a8f4169-5a95-43c3-95e1-ac0ca20a5939"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Feedback"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f576e87b-cd7b-4966-a8c0-2e45645cd86f"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Feedback"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a958352-9f8c-47e2-9f0c-4525686a011b"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9701e430-865c-4f2b-bd23-cf4d9c60b700"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1492e96-8f06-47b8-9e18-a9a8cd2d9b44"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af727628-73d6-4ebd-918b-3f003aad2bce"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec40ab26-d7e6-4c16-8570-171ee1be18ea"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60fda9f6-ef86-42b0-88c0-3b3a66b228b5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fba4bb6-5808-4881-bf7a-50c712f93239"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86b1b986-92e9-4bb6-bb2b-f750a6f43958"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90ee1f95-fe47-4f7e-bb28-5ed2d9321886"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4071216-1b38-4055-9709-f911738e022f"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89e38039-be82-4482-9cbf-66ab038ba020"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bee89b19-3a48-47f7-8aaa-af3b7c23c74a"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55240c7b-a97d-4c46-bd8c-1741dc4b492a"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Key"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialog"",
            ""id"": ""d276d8d0-4c4d-49ca-8a8b-edd7763229bd"",
            ""actions"": [
                {
                    ""name"": ""Continue"",
                    ""type"": ""Button"",
                    ""id"": ""25734bd5-5033-4671-aca5-966273a498d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Any Input"",
                    ""type"": ""Button"",
                    ""id"": ""9a73711e-5ef4-4f90-af12-5bcad7bddad9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0b3f608b-497e-4ac4-a746-8e494c93055b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbceea12-57a8-45c8-b2b1-079ba588ef43"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5d647c2-b858-4fab-99e0-d608d053765f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0014d10d-bb97-43f4-bc76-26e699f2cd8b"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any Input"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49c1dfeb-a2d6-4dd8-9b35-53b89c6f274e"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any Input"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e01122c-59bc-4d89-a1ae-02a4d00c5933"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any Input"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e721667-69de-49a5-8db5-d3887ec685f3"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any Input"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f1047d2-e385-4125-a939-c16d899850b2"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any Input"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a05fc07-737a-4a2f-a60e-f5e68d3ccbb4"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any Input"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccf76ea8-a3ef-4139-b598-0bc4f6c24f87"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any Input"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Cutscene"",
            ""id"": ""210ba231-d773-46c6-a081-6869c7c1e63f"",
            ""actions"": [
                {
                    ""name"": ""Continue"",
                    ""type"": ""Button"",
                    ""id"": ""51df9810-604a-4cd9-a1a3-a93e3199c354"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ab19f5b6-9c14-4ad1-a286-5543fa370911"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a0f9695-fc9f-480f-8836-b4f02d95600f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MK Input"",
            ""id"": ""b3626367-d975-4854-8cae-fb0cbdcd8bef"",
            ""actions"": [
                {
                    ""name"": ""Key"",
                    ""type"": ""Button"",
                    ""id"": ""cfc97e3a-caa1-4262-8159-0f3a897eddaf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""079e4706-ffd1-4984-8199-5d6c616c9b03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ae1f949a-3920-411d-85c0-c453152af195"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Key"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f64582f-9e6a-4a8c-8e1b-1c6cdeafb38f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffb6a8c7-842d-4733-9d59-9c35165349f8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad Input"",
            ""id"": ""44fe2b58-8a5c-430d-b690-400bde36f1d6"",
            ""actions"": [
                {
                    ""name"": ""Left Stick"",
                    ""type"": ""Button"",
                    ""id"": ""77656eab-58f4-4e60-96ac-207982825071"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button"",
                    ""type"": ""Button"",
                    ""id"": ""969b9546-b58b-4f40-a5b5-15a5c8158ad0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5eb16f3e-b453-4f62-b4d9-d0d966caf783"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""addfe9ad-3c95-484c-8537-702233cea804"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5bc7c11-db19-4b40-ba9d-77aa0d5c0e48"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""447ba48b-34d4-4135-9c0a-cb1860760279"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f8a958b-6844-4234-ba95-8db116139464"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce682788-eaca-4914-81af-9e50eec69d4e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5f680f6-be48-47d1-852c-b9949f7c9468"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5640c6c2-5949-467a-ab91-5bd36f9dbe5c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06d29f63-48ca-447a-a4cf-9cfb1f9626e4"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5d1ea16-92d7-49e2-993b-edcca8cc4ecf"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27d3cde6-1d1b-44b7-952c-cd98d6c7844a"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c47e0c56-e55c-4d75-88ee-2543aaeb5d78"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_Feedback = m_Gameplay.FindAction("Feedback", throwIfNotFound: true);
        m_Gameplay_LeftStick = m_Gameplay.FindAction("Left Stick", throwIfNotFound: true);
        m_Gameplay_Button = m_Gameplay.FindAction("Button", throwIfNotFound: true);
        m_Gameplay_Key = m_Gameplay.FindAction("Key", throwIfNotFound: true);
        // Dialog
        m_Dialog = asset.FindActionMap("Dialog", throwIfNotFound: true);
        m_Dialog_Continue = m_Dialog.FindAction("Continue", throwIfNotFound: true);
        m_Dialog_AnyInput = m_Dialog.FindAction("Any Input", throwIfNotFound: true);
        // Cutscene
        m_Cutscene = asset.FindActionMap("Cutscene", throwIfNotFound: true);
        m_Cutscene_Continue = m_Cutscene.FindAction("Continue", throwIfNotFound: true);
        // MK Input
        m_MKInput = asset.FindActionMap("MK Input", throwIfNotFound: true);
        m_MKInput_Key = m_MKInput.FindAction("Key", throwIfNotFound: true);
        m_MKInput_Mouse = m_MKInput.FindAction("Mouse", throwIfNotFound: true);
        // Gamepad Input
        m_GamepadInput = asset.FindActionMap("Gamepad Input", throwIfNotFound: true);
        m_GamepadInput_LeftStick = m_GamepadInput.FindAction("Left Stick", throwIfNotFound: true);
        m_GamepadInput_Button = m_GamepadInput.FindAction("Button", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_Feedback;
    private readonly InputAction m_Gameplay_LeftStick;
    private readonly InputAction m_Gameplay_Button;
    private readonly InputAction m_Gameplay_Key;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @Feedback => m_Wrapper.m_Gameplay_Feedback;
        public InputAction @LeftStick => m_Wrapper.m_Gameplay_LeftStick;
        public InputAction @Button => m_Wrapper.m_Gameplay_Button;
        public InputAction @Key => m_Wrapper.m_Gameplay_Key;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Feedback.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFeedback;
                @Feedback.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFeedback;
                @Feedback.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFeedback;
                @LeftStick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftStick;
                @LeftStick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftStick;
                @LeftStick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftStick;
                @Button.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButton;
                @Button.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButton;
                @Button.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButton;
                @Key.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKey;
                @Key.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKey;
                @Key.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKey;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Feedback.started += instance.OnFeedback;
                @Feedback.performed += instance.OnFeedback;
                @Feedback.canceled += instance.OnFeedback;
                @LeftStick.started += instance.OnLeftStick;
                @LeftStick.performed += instance.OnLeftStick;
                @LeftStick.canceled += instance.OnLeftStick;
                @Button.started += instance.OnButton;
                @Button.performed += instance.OnButton;
                @Button.canceled += instance.OnButton;
                @Key.started += instance.OnKey;
                @Key.performed += instance.OnKey;
                @Key.canceled += instance.OnKey;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Dialog
    private readonly InputActionMap m_Dialog;
    private IDialogActions m_DialogActionsCallbackInterface;
    private readonly InputAction m_Dialog_Continue;
    private readonly InputAction m_Dialog_AnyInput;
    public struct DialogActions
    {
        private @PlayerControls m_Wrapper;
        public DialogActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Continue => m_Wrapper.m_Dialog_Continue;
        public InputAction @AnyInput => m_Wrapper.m_Dialog_AnyInput;
        public InputActionMap Get() { return m_Wrapper.m_Dialog; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogActions set) { return set.Get(); }
        public void SetCallbacks(IDialogActions instance)
        {
            if (m_Wrapper.m_DialogActionsCallbackInterface != null)
            {
                @Continue.started -= m_Wrapper.m_DialogActionsCallbackInterface.OnContinue;
                @Continue.performed -= m_Wrapper.m_DialogActionsCallbackInterface.OnContinue;
                @Continue.canceled -= m_Wrapper.m_DialogActionsCallbackInterface.OnContinue;
                @AnyInput.started -= m_Wrapper.m_DialogActionsCallbackInterface.OnAnyInput;
                @AnyInput.performed -= m_Wrapper.m_DialogActionsCallbackInterface.OnAnyInput;
                @AnyInput.canceled -= m_Wrapper.m_DialogActionsCallbackInterface.OnAnyInput;
            }
            m_Wrapper.m_DialogActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Continue.started += instance.OnContinue;
                @Continue.performed += instance.OnContinue;
                @Continue.canceled += instance.OnContinue;
                @AnyInput.started += instance.OnAnyInput;
                @AnyInput.performed += instance.OnAnyInput;
                @AnyInput.canceled += instance.OnAnyInput;
            }
        }
    }
    public DialogActions @Dialog => new DialogActions(this);

    // Cutscene
    private readonly InputActionMap m_Cutscene;
    private ICutsceneActions m_CutsceneActionsCallbackInterface;
    private readonly InputAction m_Cutscene_Continue;
    public struct CutsceneActions
    {
        private @PlayerControls m_Wrapper;
        public CutsceneActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Continue => m_Wrapper.m_Cutscene_Continue;
        public InputActionMap Get() { return m_Wrapper.m_Cutscene; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CutsceneActions set) { return set.Get(); }
        public void SetCallbacks(ICutsceneActions instance)
        {
            if (m_Wrapper.m_CutsceneActionsCallbackInterface != null)
            {
                @Continue.started -= m_Wrapper.m_CutsceneActionsCallbackInterface.OnContinue;
                @Continue.performed -= m_Wrapper.m_CutsceneActionsCallbackInterface.OnContinue;
                @Continue.canceled -= m_Wrapper.m_CutsceneActionsCallbackInterface.OnContinue;
            }
            m_Wrapper.m_CutsceneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Continue.started += instance.OnContinue;
                @Continue.performed += instance.OnContinue;
                @Continue.canceled += instance.OnContinue;
            }
        }
    }
    public CutsceneActions @Cutscene => new CutsceneActions(this);

    // MK Input
    private readonly InputActionMap m_MKInput;
    private IMKInputActions m_MKInputActionsCallbackInterface;
    private readonly InputAction m_MKInput_Key;
    private readonly InputAction m_MKInput_Mouse;
    public struct MKInputActions
    {
        private @PlayerControls m_Wrapper;
        public MKInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Key => m_Wrapper.m_MKInput_Key;
        public InputAction @Mouse => m_Wrapper.m_MKInput_Mouse;
        public InputActionMap Get() { return m_Wrapper.m_MKInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MKInputActions set) { return set.Get(); }
        public void SetCallbacks(IMKInputActions instance)
        {
            if (m_Wrapper.m_MKInputActionsCallbackInterface != null)
            {
                @Key.started -= m_Wrapper.m_MKInputActionsCallbackInterface.OnKey;
                @Key.performed -= m_Wrapper.m_MKInputActionsCallbackInterface.OnKey;
                @Key.canceled -= m_Wrapper.m_MKInputActionsCallbackInterface.OnKey;
                @Mouse.started -= m_Wrapper.m_MKInputActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_MKInputActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_MKInputActionsCallbackInterface.OnMouse;
            }
            m_Wrapper.m_MKInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Key.started += instance.OnKey;
                @Key.performed += instance.OnKey;
                @Key.canceled += instance.OnKey;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
            }
        }
    }
    public MKInputActions @MKInput => new MKInputActions(this);

    // Gamepad Input
    private readonly InputActionMap m_GamepadInput;
    private IGamepadInputActions m_GamepadInputActionsCallbackInterface;
    private readonly InputAction m_GamepadInput_LeftStick;
    private readonly InputAction m_GamepadInput_Button;
    public struct GamepadInputActions
    {
        private @PlayerControls m_Wrapper;
        public GamepadInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftStick => m_Wrapper.m_GamepadInput_LeftStick;
        public InputAction @Button => m_Wrapper.m_GamepadInput_Button;
        public InputActionMap Get() { return m_Wrapper.m_GamepadInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamepadInputActions set) { return set.Get(); }
        public void SetCallbacks(IGamepadInputActions instance)
        {
            if (m_Wrapper.m_GamepadInputActionsCallbackInterface != null)
            {
                @LeftStick.started -= m_Wrapper.m_GamepadInputActionsCallbackInterface.OnLeftStick;
                @LeftStick.performed -= m_Wrapper.m_GamepadInputActionsCallbackInterface.OnLeftStick;
                @LeftStick.canceled -= m_Wrapper.m_GamepadInputActionsCallbackInterface.OnLeftStick;
                @Button.started -= m_Wrapper.m_GamepadInputActionsCallbackInterface.OnButton;
                @Button.performed -= m_Wrapper.m_GamepadInputActionsCallbackInterface.OnButton;
                @Button.canceled -= m_Wrapper.m_GamepadInputActionsCallbackInterface.OnButton;
            }
            m_Wrapper.m_GamepadInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftStick.started += instance.OnLeftStick;
                @LeftStick.performed += instance.OnLeftStick;
                @LeftStick.canceled += instance.OnLeftStick;
                @Button.started += instance.OnButton;
                @Button.performed += instance.OnButton;
                @Button.canceled += instance.OnButton;
            }
        }
    }
    public GamepadInputActions @GamepadInput => new GamepadInputActions(this);
    public interface IGameplayActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnFeedback(InputAction.CallbackContext context);
        void OnLeftStick(InputAction.CallbackContext context);
        void OnButton(InputAction.CallbackContext context);
        void OnKey(InputAction.CallbackContext context);
    }
    public interface IDialogActions
    {
        void OnContinue(InputAction.CallbackContext context);
        void OnAnyInput(InputAction.CallbackContext context);
    }
    public interface ICutsceneActions
    {
        void OnContinue(InputAction.CallbackContext context);
    }
    public interface IMKInputActions
    {
        void OnKey(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
    }
    public interface IGamepadInputActions
    {
        void OnLeftStick(InputAction.CallbackContext context);
        void OnButton(InputAction.CallbackContext context);
    }
}
