using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine.UI;

public enum AnimationType
{
    Fade,
    Scale,
    Rotate,
    Translate,
    Colorize,
}

[Serializable]
public class AnimationData
{
    [Tooltip("Test")]
    public AnimationType animationType;
    public List<AnimationCurve> animationCurves;
}

[Serializable]
public class AnimationLayer
{
    [Range(0, 10)]
    public float delayTime;
    public List<AnimationData> animationDataList;
}

[Serializable]
public class AnimationSequence
{
    public List<AnimationLayer> animationLayers;
}


public static class Animations
{
    //private delegate void Animate(params float[] a_Values);

    public static IEnumerator Animate<TObject>(
        AnimationSequence a_AnimationSequence,
        TObject a_Object) where TObject : Graphic
    {
        //yield return new WaitForEndOfFrame();
        foreach (AnimationLayer animationLayer in a_AnimationSequence.animationLayers)
        {
            yield return new WaitForSeconds(animationLayer.delayTime);

            yield return AnimateLayer(animationLayer, a_Object);
        }
    }

    public static IEnumerator AnimateLayer<TObject>(
        AnimationLayer a_AnimationLayer,
        TObject a_Object) where TObject : Graphic
    {
        float endTime = GetEndTime(a_AnimationLayer.animationDataList);

        float deltaTime = 0.0f;
        while (deltaTime < endTime)
        {
            deltaTime += Time.deltaTime;
            foreach (AnimationData animationData in a_AnimationLayer.animationDataList)
            {
                if(animationData.animationCurves.Count < 2)
                    animationData.animationCurves.Add(animationData.animationCurves[0]);

                switch (animationData.animationType)
                {
                    case AnimationType.Fade:
                        {
                            a_Object.color = new Color(
                                a_Object.color.r,
                                a_Object.color.g,
                                a_Object.color.b,
                                animationData.animationCurves[0].Evaluate(deltaTime));
                            
                        }
                        break;
                    case AnimationType.Scale:
                        {
                            a_Object.transform.localScale = new Vector3(
                                animationData.animationCurves[0].Evaluate(deltaTime),
                                animationData.animationCurves[1].Evaluate(deltaTime));
                        }
                        break;
                    default:
                        Debug.Log("Not Yet Implemented...");
                        break;
                }
            }
            yield return null;
        }
    }

    private static float GetEndTime(List<AnimationData> a_AnimationDataList)
    {
        List<AnimationCurve> animationCurves = new List<AnimationCurve>();

        foreach (AnimationData animationData in a_AnimationDataList)
            foreach (AnimationCurve animationCurve in animationData.animationCurves)
                animationCurves.Add(animationCurve);

        animationCurves.Sort(SortAnimationCurves);

        return animationCurves[0][animationCurves[0].length - 1].time;
    }

    private static int SortAnimationCurves(AnimationCurve a, AnimationCurve b)
    {
        if (a[a.length - 1].time < b[b.length - 1].time)
            return 1;
        if (a[a.length - 1].time > b[b.length - 1].time)
            return -1;

        return 0;
    }
    //public static void AnimateValue(AnimationCurve a_AnimationCurve, float a_TimeKey, out float a_Value)
    //{

    //}

    //public static IEnumerator Fade2DGraphic<TGraphic>(TGraphic a_Graphic, params AnimationCurve[] a_AnimationCurves)
    //    where TGraphic : MaskableGraphic
    //{
    //    Keyframe lastKeyframe;

    //    if (!ParseKeyframe(out lastKeyframe, ref a_AnimationCurves))
    //        yield return true;

    //    float deltaTime = 0.0f;
    //    while (deltaTime < lastKeyframe.time)
    //    {
    //        deltaTime += Time.deltaTime;
    //        a_Graphic.color =
    //            new Color(
    //                a_Graphic.color.r,
    //                a_Graphic.color.g,
    //                a_Graphic.color.b,
    //                a_AnimationCurves[0].Evaluate(deltaTime));

    //        yield return false;
    //    }
    //}

    //public static IEnumerator Scale2DTransform<TTransform>(
    //    TTransform a_Transform,
    //    params AnimationCurve[] a_AnimationCurves) where TTransform : Transform
    //{
    //    Keyframe lastKeyframe;

    //    if (!ParseKeyframe(out lastKeyframe, ref a_AnimationCurves))
    //        yield return true;

    //    yield return ParseAnimation(
    //        lastKeyframe,
    //        a_AnimationCurves,
    //        delegate (float[] a_Floats)
    //        {
    //            a_Transform.localScale = new Vector3(a_Floats[0], a_Floats[1]);
    //        });
    //}

    //private static bool ParseKeyframe(out Keyframe a_LastKeyframe, ref AnimationCurve[] a_AnimationCurves)
    //{
    //    a_LastKeyframe = new Keyframe();
    //    if (a_AnimationCurves.Length == 0)
    //    {
    //        Debug.LogError("'Animations' was not given any animation curves");
    //        return false;
    //    }

    //    if (a_AnimationCurves.Length > 2)
    //    {
    //        Debug.LogError("'Animations' cannot handle more than 2 animation curves");
    //        return false;
    //    }

    //    switch (a_AnimationCurves.Length)
    //    {
    //        case 1:
    //            a_LastKeyframe = a_AnimationCurves[0][a_AnimationCurves[0].length - 1];
    //            break;

    //        default:
    //            a_LastKeyframe =
    //                a_AnimationCurves[0].length > a_AnimationCurves[1].length
    //                    ? a_AnimationCurves[0][a_AnimationCurves[0].length - 1]
    //                    : a_AnimationCurves[1][a_AnimationCurves[1].length - 1];
    //            break;
    //    }
    //    return true;
    //}

    //private static IEnumerator ParseAnimation(Keyframe a_Keyframe, AnimationCurve[] a_AnimationCurves, Animate a_Delegate)
    //{
    //    float deltaTime = 0.0f;
    //    while (deltaTime < a_Keyframe.time)
    //    {
    //        deltaTime += Time.deltaTime;
    //        switch (a_AnimationCurves.Length)
    //        {
    //            case 1:
    //                a_Delegate(a_AnimationCurves[0].Evaluate(deltaTime), a_AnimationCurves[0].Evaluate(deltaTime));
    //                break;
    //            default:
    //                a_Delegate(a_AnimationCurves[0].Evaluate(deltaTime), a_AnimationCurves[1].Evaluate(deltaTime));
    //                break;
    //        }
    //        yield return false;
    //    }
    //}
}
