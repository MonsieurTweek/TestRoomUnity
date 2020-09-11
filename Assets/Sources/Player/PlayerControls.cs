// GENERATED AUTOMATICALLY FROM 'Assets/Sources/Input/PlayerControls.inputactions'

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
            ""id"": ""22414e80-0954-4d46-ba4e-b73ca6185045"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""3af412bc-734a-49fc-83d6-91c6403e4854"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""062d3dfe-6703-4cb8-8a42-fa50a707e795"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Strafe"",
                    ""type"": ""Value"",
                    ""id"": ""cb95086c-8e28-4936-9049-7b547b4f0396"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""08b5e72d-51cf-4fbc-b758-5a07c090f024"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""51f8ec3e-4e43-43ac-9d93-364779c748d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Target"",
                    ""type"": ""Button"",
                    ""id"": ""3909c319-3272-49c5-94a6-a83c6b941ed8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack Light"",
                    ""type"": ""Button"",
                    ""id"": ""3c77bfe7-5ead-4796-86b9-63663b672588"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack Heavy"",
                    ""type"": ""Button"",
                    ""id"": ""0939fd67-e8a3-449e-a0ac-821b25508ebf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Left Stick Horizontal [Gamepad]"",
                    ""id"": ""4fe3611f-3ac1-42b4-991b-7d06a7fa9823"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""BinaryInput"",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""6be1d0a1-4e5c-4761-98a8-880f9a4b07ec"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""c4bb38ce-0feb-4b0d-ac13-fb362269d6c5"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ZQSD [Keyboard]"",
                    ""id"": ""e0b32741-8ffb-4ac2-ba4c-5878295c61b9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""dabfbfd1-fe40-48a4-87fd-e06f164b3424"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""3b4907ae-59d5-4284-a253-af8c7d8c8489"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows [Keyboard]"",
                    ""id"": ""454bbc65-2742-4728-95e5-0d30a38ec774"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""bcc49770-e264-4c05-a87a-24044c465c35"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""ddba2a54-300d-427d-b7f1-da173e565f81"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""be3ab57e-f717-4c3d-a89e-e79ec808ded6"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1968448-271e-424e-89dd-2825a790b418"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d6005e7-6906-4ad0-81b5-4c8bd7e96bfb"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Target"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d93e6b0-7797-4e68-816c-5547432f0228"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Target"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30f37e9b-c7b4-40e8-bfc3-f5cac3901c4c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70ef0e32-45a3-4fd8-a214-3eaee493fe83"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d50fa0b8-9605-4c5f-a2ff-ea907c265cea"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack Heavy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdbeebb3-beb5-4b1c-b0df-ebc07cebec43"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack Heavy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Left Stick Horizontal [Gamepad]"",
                    ""id"": ""2e652919-d266-46b2-a685-aae1ce036dd7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""BinaryInput"",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bff11d95-f360-4d1d-8656-bdecefa3bcb4"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8ea4333c-6540-4c62-85c2-81695614df51"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""QD [Keyboard]"",
                    ""id"": ""4133afae-64bf-472f-975f-3a619b40013d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c5ff5de1-219b-4dc0-a777-d6ff7ecd2536"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""70575c57-07c8-490a-9bce-4b65aa974763"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows [Keyboard]"",
                    ""id"": ""3b3e9516-31f4-4484-9e61-dfff83bdc346"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f03881c5-ef22-4734-8bab-abc3ab07ca7d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ed7ff1eb-0018-469c-ac28-3b0d7808cc75"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3f77b5fc-1f52-45aa-b501-fa1f73512b5e"",
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
                    ""id"": ""91546a6b-510b-454d-819c-39a1822b9088"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Left Stick Vertical [Gamepad]"",
                    ""id"": ""9cb94f45-0e35-49bc-9978-8ac6b95d34bb"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""BinaryInput"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ce2461ec-3418-4ccc-93de-830661c1cf57"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4e5eb673-ec54-49c9-ae06-53b30764b09d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ZQSD [Keyboard]"",
                    ""id"": ""77d4b63c-9e56-4a02-b8e0-988c75526676"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bfc3f43b-c19b-42c1-801f-b7b7557734a1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ab78f2af-daab-47fb-842d-d2d1b947a9a8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows [Keyboard]"",
                    ""id"": ""803f4e66-402d-4e4b-974f-97ef129752cf"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1391097b-d35b-4cd5-8a35-5f5515e1b072"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f11a15b6-ead8-41e7-96b1-fa6c8e360be2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""f70e32cf-1d88-4db0-91ab-e85cde97c69e"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""9fe98f9d-5d6d-4ce4-945c-8bf441d3187c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch"",
                    ""type"": ""Value"",
                    ""id"": ""7c3543d3-c038-4cec-be49-e49c34a8dd16"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""b9ba6def-8273-4ee7-9656-026e8e326e48"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""9aa1fba5-0a62-4ced-9868-d47140711a80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ping_Keyboard"",
                    ""type"": ""Value"",
                    ""id"": ""40ff8ac1-9845-4dd2-ba7d-80d3cf6341c9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ping_Gamepad"",
                    ""type"": ""Value"",
                    ""id"": ""9ff98304-efef-4b87-8751-1badc8267162"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6dfddcc6-01ae-4aaa-9396-c46b2978de7c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""111c68fa-0296-4f21-8a1b-b7ef7264713b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Shoulder [Gamepad]"",
                    ""id"": ""35510187-168e-4fe9-a4f4-d8210732f166"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""651a3bbc-7337-4cd4-8c24-a7c15256c75a"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5ab7fc19-327a-4b8d-b5d7-548629157b17"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ZQSD [Keyboard]"",
                    ""id"": ""e58a0fb2-7cd8-4fcb-91a7-e5ccc6b2c10b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""972b48ae-554c-472a-a1ab-b5553c950fc2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f5277494-e626-446e-a3d6-be5798f23589"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""db46c9b9-f6b0-44fb-b41f-b7fc32c77e96"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edd0fe3c-552f-49c9-992d-3d50cb3db221"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ZQSD [Keyboard]"",
                    ""id"": ""a05320b7-e653-4442-b25a-6dc29502361a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""438db2ba-ec33-44dd-8ffb-092444eaec8f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e9d0eec4-0b99-48c6-8edc-6c608565d797"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8b991220-70ca-42ca-8664-a9a7e2d2d437"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f98c864a-4e2a-431f-8e8d-ddea548effb6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows [Keyboard]"",
                    ""id"": ""5475fb10-0854-4c7b-b18c-716ccc1a7b05"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7bd658b0-a61f-4ac5-a6c8-2eea8083c10d"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a72c7177-4665-442a-8b89-578eed8c2fe2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c39e07ee-f059-40be-9fd6-552ee3372422"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""74ac4418-56bf-49a7-944a-728708574b39"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1d2b6905-f83f-4b14-ad54-56309b721d54"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bd02bbc-b326-49ca-8e9d-b3a160a6725b"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5650c8f0-c153-42d2-bc24-ebb93e6c1057"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c37b68c1-155d-44dd-a703-fa8dca690a18"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a98e3c62-a9f5-48dd-aed5-d567495c4817"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1c146fe-9f50-4cfa-b5d5-d37c0166cee3"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f4cb359-5ef0-4f3b-9098-f21e2581d27d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06708f8b-e21a-436a-8d25-1daf07756805"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfd29529-25e8-4e8e-a0d3-3089f7dc3a96"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77213035-837b-412f-afa4-9ba881c931ba"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8c0ecad-2bf8-42c7-ae73-0c940906e8ea"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8f7a47c-1df0-4114-a907-f8b89ca68dd7"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfe8b86c-8596-4365-8b3c-572db3e42b77"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad864921-aa1d-4f83-88ca-87136aaa0598"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e474120a-6fca-4b68-a436-acc4e68213a1"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7eb529ca-dcfc-48e0-a7e5-13435b3ced8d"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ping_Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Cheat"",
            ""id"": ""a5b5d4fd-2626-408a-83e3-ca7b771a1985"",
            ""actions"": [
                {
                    ""name"": ""Kill_Player"",
                    ""type"": ""Button"",
                    ""id"": ""d3b1c76f-14af-43ac-86ae-280b032b5632"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Kill_Enemy"",
                    ""type"": ""Button"",
                    ""id"": ""6971f030-f83f-4516-9ebf-51da3a7aea51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Klapaucius"",
                    ""type"": ""Button"",
                    ""id"": ""f706f130-9da2-469b-9a6e-7c48b6732adb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""536c7763-a21c-4667-9e5b-66b73d6d69b1"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Kill_Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23fb1d58-6bda-4bb3-9599-595b04f4100e"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Kill_Enemy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6880790-d768-4d73-95fc-37ea5349045b"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Klapaucius"",
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
        m_Gameplay_Rotate = m_Gameplay.FindAction("Rotate", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Strafe = m_Gameplay.FindAction("Strafe", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Dash = m_Gameplay.FindAction("Dash", throwIfNotFound: true);
        m_Gameplay_Target = m_Gameplay.FindAction("Target", throwIfNotFound: true);
        m_Gameplay_AttackLight = m_Gameplay.FindAction("Attack Light", throwIfNotFound: true);
        m_Gameplay_AttackHeavy = m_Gameplay.FindAction("Attack Heavy", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Navigate = m_Menu.FindAction("Navigate", throwIfNotFound: true);
        m_Menu_Switch = m_Menu.FindAction("Switch", throwIfNotFound: true);
        m_Menu_Cancel = m_Menu.FindAction("Cancel", throwIfNotFound: true);
        m_Menu_Confirm = m_Menu.FindAction("Confirm", throwIfNotFound: true);
        m_Menu_Ping_Keyboard = m_Menu.FindAction("Ping_Keyboard", throwIfNotFound: true);
        m_Menu_Ping_Gamepad = m_Menu.FindAction("Ping_Gamepad", throwIfNotFound: true);
        // Cheat
        m_Cheat = asset.FindActionMap("Cheat", throwIfNotFound: true);
        m_Cheat_Kill_Player = m_Cheat.FindAction("Kill_Player", throwIfNotFound: true);
        m_Cheat_Kill_Enemy = m_Cheat.FindAction("Kill_Enemy", throwIfNotFound: true);
        m_Cheat_Klapaucius = m_Cheat.FindAction("Klapaucius", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_Rotate;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Strafe;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Dash;
    private readonly InputAction m_Gameplay_Target;
    private readonly InputAction m_Gameplay_AttackLight;
    private readonly InputAction m_Gameplay_AttackHeavy;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Gameplay_Rotate;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Strafe => m_Wrapper.m_Gameplay_Strafe;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputAction @Target => m_Wrapper.m_Gameplay_Target;
        public InputAction @AttackLight => m_Wrapper.m_Gameplay_AttackLight;
        public InputAction @AttackHeavy => m_Wrapper.m_Gameplay_AttackHeavy;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Strafe.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStrafe;
                @Strafe.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStrafe;
                @Strafe.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStrafe;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Target.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTarget;
                @Target.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTarget;
                @Target.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTarget;
                @AttackLight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttackLight;
                @AttackLight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttackLight;
                @AttackLight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttackLight;
                @AttackHeavy.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttackHeavy;
                @AttackHeavy.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttackHeavy;
                @AttackHeavy.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttackHeavy;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Strafe.started += instance.OnStrafe;
                @Strafe.performed += instance.OnStrafe;
                @Strafe.canceled += instance.OnStrafe;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Target.started += instance.OnTarget;
                @Target.performed += instance.OnTarget;
                @Target.canceled += instance.OnTarget;
                @AttackLight.started += instance.OnAttackLight;
                @AttackLight.performed += instance.OnAttackLight;
                @AttackLight.canceled += instance.OnAttackLight;
                @AttackHeavy.started += instance.OnAttackHeavy;
                @AttackHeavy.performed += instance.OnAttackHeavy;
                @AttackHeavy.canceled += instance.OnAttackHeavy;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Navigate;
    private readonly InputAction m_Menu_Switch;
    private readonly InputAction m_Menu_Cancel;
    private readonly InputAction m_Menu_Confirm;
    private readonly InputAction m_Menu_Ping_Keyboard;
    private readonly InputAction m_Menu_Ping_Gamepad;
    public struct MenuActions
    {
        private @PlayerControls m_Wrapper;
        public MenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Menu_Navigate;
        public InputAction @Switch => m_Wrapper.m_Menu_Switch;
        public InputAction @Cancel => m_Wrapper.m_Menu_Cancel;
        public InputAction @Confirm => m_Wrapper.m_Menu_Confirm;
        public InputAction @Ping_Keyboard => m_Wrapper.m_Menu_Ping_Keyboard;
        public InputAction @Ping_Gamepad => m_Wrapper.m_Menu_Ping_Gamepad;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                @Switch.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSwitch;
                @Switch.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSwitch;
                @Switch.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSwitch;
                @Cancel.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                @Confirm.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Ping_Keyboard.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPing_Keyboard;
                @Ping_Keyboard.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPing_Keyboard;
                @Ping_Keyboard.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPing_Keyboard;
                @Ping_Gamepad.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPing_Gamepad;
                @Ping_Gamepad.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPing_Gamepad;
                @Ping_Gamepad.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPing_Gamepad;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Switch.started += instance.OnSwitch;
                @Switch.performed += instance.OnSwitch;
                @Switch.canceled += instance.OnSwitch;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Ping_Keyboard.started += instance.OnPing_Keyboard;
                @Ping_Keyboard.performed += instance.OnPing_Keyboard;
                @Ping_Keyboard.canceled += instance.OnPing_Keyboard;
                @Ping_Gamepad.started += instance.OnPing_Gamepad;
                @Ping_Gamepad.performed += instance.OnPing_Gamepad;
                @Ping_Gamepad.canceled += instance.OnPing_Gamepad;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // Cheat
    private readonly InputActionMap m_Cheat;
    private ICheatActions m_CheatActionsCallbackInterface;
    private readonly InputAction m_Cheat_Kill_Player;
    private readonly InputAction m_Cheat_Kill_Enemy;
    private readonly InputAction m_Cheat_Klapaucius;
    public struct CheatActions
    {
        private @PlayerControls m_Wrapper;
        public CheatActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Kill_Player => m_Wrapper.m_Cheat_Kill_Player;
        public InputAction @Kill_Enemy => m_Wrapper.m_Cheat_Kill_Enemy;
        public InputAction @Klapaucius => m_Wrapper.m_Cheat_Klapaucius;
        public InputActionMap Get() { return m_Wrapper.m_Cheat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatActions set) { return set.Get(); }
        public void SetCallbacks(ICheatActions instance)
        {
            if (m_Wrapper.m_CheatActionsCallbackInterface != null)
            {
                @Kill_Player.started -= m_Wrapper.m_CheatActionsCallbackInterface.OnKill_Player;
                @Kill_Player.performed -= m_Wrapper.m_CheatActionsCallbackInterface.OnKill_Player;
                @Kill_Player.canceled -= m_Wrapper.m_CheatActionsCallbackInterface.OnKill_Player;
                @Kill_Enemy.started -= m_Wrapper.m_CheatActionsCallbackInterface.OnKill_Enemy;
                @Kill_Enemy.performed -= m_Wrapper.m_CheatActionsCallbackInterface.OnKill_Enemy;
                @Kill_Enemy.canceled -= m_Wrapper.m_CheatActionsCallbackInterface.OnKill_Enemy;
                @Klapaucius.started -= m_Wrapper.m_CheatActionsCallbackInterface.OnKlapaucius;
                @Klapaucius.performed -= m_Wrapper.m_CheatActionsCallbackInterface.OnKlapaucius;
                @Klapaucius.canceled -= m_Wrapper.m_CheatActionsCallbackInterface.OnKlapaucius;
            }
            m_Wrapper.m_CheatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Kill_Player.started += instance.OnKill_Player;
                @Kill_Player.performed += instance.OnKill_Player;
                @Kill_Player.canceled += instance.OnKill_Player;
                @Kill_Enemy.started += instance.OnKill_Enemy;
                @Kill_Enemy.performed += instance.OnKill_Enemy;
                @Kill_Enemy.canceled += instance.OnKill_Enemy;
                @Klapaucius.started += instance.OnKlapaucius;
                @Klapaucius.performed += instance.OnKlapaucius;
                @Klapaucius.canceled += instance.OnKlapaucius;
            }
        }
    }
    public CheatActions @Cheat => new CheatActions(this);
    public interface IGameplayActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnStrafe(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnTarget(InputAction.CallbackContext context);
        void OnAttackLight(InputAction.CallbackContext context);
        void OnAttackHeavy(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
        void OnPing_Keyboard(InputAction.CallbackContext context);
        void OnPing_Gamepad(InputAction.CallbackContext context);
    }
    public interface ICheatActions
    {
        void OnKill_Player(InputAction.CallbackContext context);
        void OnKill_Enemy(InputAction.CallbackContext context);
        void OnKlapaucius(InputAction.CallbackContext context);
    }
}
