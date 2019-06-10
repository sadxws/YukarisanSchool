﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utage;
using System;
using Def.Enum;

public class UtageSendMessage : MonoBehaviour
{
    public LineController lineController_;
         
    public LineRenderController lineRenderController_;
    private enumUtageSendMessage enumUtageSendMessage_;
    private bool isWaite_ = true;

    //SendMessageコマンドが実行されたタイミング
    private void OnDoCommand(AdvCommandSendMessage command)
    {
        Enum.TryParse(command.Name, out enumUtageSendMessage_);
        isWaite_ = true;

        switch (enumUtageSendMessage_)
        {
            case enumUtageSendMessage.UnityFadeIn:
                StartCoroutine(FadeMan.ins_.FadeInOut(enumFadeType.FadeIn, float.Parse(command.Arg2), () => { isWaite_ = false; }));
                break;

            case enumUtageSendMessage.UnityFadeOut:
                StartCoroutine(FadeMan.ins_.FadeInOut(enumFadeType.FadeOut, float.Parse(command.Arg2), () => { isWaite_ = false; }));
                break;

            case enumUtageSendMessage.UnityLinesSet:

                break;

            case enumUtageSendMessage.UnityLinesTween:
                string lineId = string.Empty;
                float tweenTime;
                string fadeType = string.Empty;
                Vector2 pos01;
                Vector2 pos02;

                isWaite_ = (bool.Parse)(command.Arg2);
                string[] str = command.Arg3.Split(',');
                lineId = str[0];
                tweenTime = float.Parse(str[1]);
                fadeType = command.Arg4;
                ConvertStringToVec2(command.Arg5, out pos01, out pos02);

                break;

            case enumUtageSendMessage.UnityLinesClean:
                lineController_.CleanAllLine();
                 break;

            default:
                Debug.Log("<color=red>존재하지 않는 메소드:" + command.Name + "</color>");
                break;
        }
    }

    //SendMessageコマンドの処理待ちタイミング
    private void OnWait(AdvCommandSendMessage command)
    {
        Enum.TryParse(command.Name, out enumUtageSendMessage_);
        switch (enumUtageSendMessage_)
        {
            case enumUtageSendMessage.UnityFadeIn:
                command.IsWait = isWaite_;
                break;
            case enumUtageSendMessage.UnityFadeOut:
                command.IsWait = isWaite_;
                break;

            case enumUtageSendMessage.UnityLinesSet:
                command.IsWait = isWaite_;
                break;

            case enumUtageSendMessage.UnityLinesTween:
                command.IsWait = isWaite_;
                break;

            case enumUtageSendMessage.UnityLinesClean:
                command.IsWait = false;
                break;

            default:
                Debug.Log("<color=red>존재하지 않는 메소드:" + command.Name + "</color>");
                command.IsWait = false;
                break;
        }       
    }

    /// <summary> 두 점을 이동이켜 선을 만든다.</summary>
    /// <param name="id">통제용 아이디</param>
    /// <param name="fadeType">페이드 유형</param>
    /// <param name="pos01">첫번째 점의 위치</param>
    /// <param name="pos02">두번째 점의 위치</param>
    /// <param name="time">트윈 시간</param>
    private void DrawLines(string id, Vector2 pos01, Vector2 pos02)
    {
        lineController_.DrawLine(id, pos01, pos02, () => { isWaite_ = false; });
    }

    private void SetLines(string id, float time, string fadeType)
    {

    }


    private void ConvertStringToVec2(string str, out Vector2 pos01, out Vector2 pos02)
    {
        string[] pos;
        str = str.Replace("(", string.Empty);
        str = str.Replace(")", string.Empty);
        pos = str.Split('/');
        pos01 = new Vector2(float.Parse(pos[0].Split(',')[0]), float.Parse(pos[0].Split(',')[1]));
        pos02 = new Vector2(float.Parse(pos[1].Split(',')[0]), float.Parse(pos[1].Split(',')[1]));
    }
}
