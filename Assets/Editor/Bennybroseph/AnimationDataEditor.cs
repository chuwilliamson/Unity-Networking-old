using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(AnimationData))]
    public class AnimationDataEditor : PropertyDrawer
    {
        /// <summary>
        /// This is how you draw to the inspector. You aren't really limited to any specific thing 
        /// as images can be drawn. The only limit is how terrible is it to try to place everything
        /// just so. Not to mention you still have to figure out the height of the stuff you just 
        /// drew. Probably the worst API I've seen from Unity.
        /// </summary>
        /// <param name="a_Position"> The position this property should start drawing to </param>
        /// <param name="a_Property"> The property to be drawn </param>
        /// <param name="a_Label"> The name of the property. Seems useless </param>
        public override void OnGUI(Rect a_Position, SerializedProperty a_Property, GUIContent a_Label)
        {
            // Store the value of the original indent to be restored later
            int originalIndent = EditorGUI.indentLevel;

            // Let Unity know you are starting this property
            EditorGUI.BeginProperty(a_Position, a_Label, a_Property);
            {
                // Set a variable to hold the foldout label and set it to this property's display name by default
                string name = a_Property.displayName;
                // if this property is part of an array
                if (a_Property.displayName.Substring(0, 7) == "Element")
                    // Parse the array index from the string Unity gives you and concatenate it to "Animation "
                    name = "Animation " + (int.Parse(a_Property.displayName.Substring(8)) + 1);

                // Set the size of the foldout, create it, and store its return value into this property 
                a_Position.size = new Vector2(Globals.FOLDOUT_WIDTH, Globals.DEFAULT_HEIGHT);
                a_Property.isExpanded = EditorGUI.Foldout(a_Position, a_Property.isExpanded, GUIContent.none);

                // Store whether the foldout is expanded to be used later
                bool isExpanded = a_Property.isExpanded;

                a_Property = a_Property.FindPropertyRelative("animationType");

                // Sets the correct amount of space after a label even after inspector resizing
                // It's empty because we want to use new this position without displaying information
                a_Position = EditorGUI.PrefixLabel(a_Position, new GUIContent(name));

                EditorGUI.indentLevel = 0;
                a_Position.size = new Vector2((Screen.width - a_Position.x) / 3, Globals.DEFAULT_HEIGHT);
                EditorGUI.PropertyField(a_Position, a_Property, GUIContent.none);

                // Move to the 'AnimationCurves' array: 'animationCurves'
                a_Property.Next(true);

                // Set the size of and create button that will increase the size of the 'animationLayers' array
                a_Position.x += a_Position.width + Globals.SPACING_WIDTH;
                a_Position.size = new Vector2(Globals.BUTTON_WIDTH, Globals.DEFAULT_HEIGHT);
                if (GUI.Button(a_Position, new GUIContent("+", "Add Animation Curve")))
                    a_Property.arraySize++;

                // Set the position of and create button that will decrease the size of the 'animationLayers' array
                a_Position.x = a_Position.x + a_Position.width + Globals.SPACING_WIDTH;
                if (GUI.Button(a_Position, new GUIContent("-", "Remove Animation Curve")))
                    a_Property.arraySize--;

                // Move past the buttons
                a_Position.x = a_Position.x + a_Position.width + Globals.SPACING_WIDTH;

                // Set up an anchor point for all the 'AnimationCurve' properties to be drawn
                Rect curvesAnchor = a_Position;
                for (int i = 0; i < a_Property.arraySize; ++i)
                {
                    if (isExpanded)
                        a_Position = GetCurvesRect(curvesAnchor, a_Property.arraySize, i);
                    else
                    {
                        a_Position.x = curvesAnchor.x + Globals.DEFAULT_HEIGHT * i;
                        a_Position.size = new Vector2(
                            Globals.DEFAULT_HEIGHT,
                            Globals.DEFAULT_HEIGHT);
                    }
                    EditorGUI.PropertyField(
                        a_Position,
                        a_Property.GetArrayElementAtIndex(i),
                        GUIContent.none);
                }

            }
            EditorGUI.EndProperty();

            EditorGUI.indentLevel = originalIndent;
        }
        /// <summary>
        /// This is where you will need to parse the property height. It's not as easy as it sounds
        /// when you have resizing elements based on conditions and other sizes. This is because this 
        /// object cannot store any member variables reliably even though it's not a static object.
        /// </summary>
        /// <param name="a_Property"> The property that needs it's height parsed from </param>
        /// <param name="a_Label"> The name of the property. Seems useless </param>
        /// <returns> The total height of the property </returns>
        public override float GetPropertyHeight(SerializedProperty a_Property, GUIContent a_Label)
        {
            // Sets up a variable to add space to our property
            float extraSpace = Globals.SPACING_HEIGHT - EditorGUIUtility.singleLineHeight;

            if (a_Property.isExpanded)
            {
                a_Property = a_Property.FindPropertyRelative("animationCurves");
                if (a_Property.arraySize > 0)
                {
                    Rect position = EditorGUI.PrefixLabel(new Rect(14, 0, 0, 0), new GUIContent(" "));
                    position.size = new Vector2((Screen.width - position.x) / 3, Globals.DEFAULT_HEIGHT);
                    position.x += position.width + Globals.SPACING_WIDTH;
                    position.size = new Vector2(Globals.BUTTON_WIDTH, Globals.DEFAULT_HEIGHT);
                    position.x = position.x + position.width + Globals.SPACING_WIDTH;
                    position.x = position.x + position.width + Globals.SPACING_WIDTH;
                    position = GetCurvesRect(position, a_Property.arraySize, 0);
                    extraSpace += position.height - Globals.DEFAULT_HEIGHT;
                }
            }

            return base.GetPropertyHeight(a_Property, a_Label) + extraSpace;
        }

        private static Rect GetCurvesRect(Rect a_Anchor, int a_Size, int a_Index)
        {
            Rect position = a_Anchor;

            position.width = (Screen.width - a_Anchor.x) / a_Size - Globals.SPACING_WIDTH;
            position.height = position.width;
            position.x += (position.width + Globals.SPACING_WIDTH) * a_Index;

            return position;
        }
    }
}
