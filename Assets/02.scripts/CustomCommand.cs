﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utage;
using System;
using Def.Enum;     

public  class CustomCommand : AdvCustomCommandManager
{
    public LineController lineController;
    public ArrowController arrowController;
    public RectController rectController;

    public static CustomCommand ins;
    private enumCustomCommand customCommand;

    public void Awake()
    {
        ins = this;
    }

    public override void OnBootInit()
    {
        Utage.AdvCommandParser.OnCreateCustomCommandFromID += CreateCustomCommand;
    }

    //AdvEnginのクリア処理のときに呼ばれる
    public override void OnClear()
    {
    }

    public Vector2 StringToVec2(string str)
    {
        string[] result;

        str = str.Replace("(", "");
        str = str.Replace(")", "");
        result = str.Split(',');

        return new Vector2(float.Parse(result[0]), float.Parse(result[1]));
    }

    public Color32 StringToColor(string str)
    {
        string[] result;
        str = str.Replace("(", "");
        str = str.Replace(")", "");
        str = str.Replace(" ", "");
        result = str.Split(',');
        return new Color32(byte.Parse(result[0]), byte.Parse(result[1]), byte.Parse(result[2]), byte.Parse(result[3]));
    }

    //カスタムコマンドの作成用コールバック
    public void CreateCustomCommand(string id, StringGridRow row, AdvSettingDataManager dataManager, ref AdvCommand command)
    {
        if (Enum.TryParse(id, out customCommand))
        {
            switch (customCommand)
            {
                case enumCustomCommand.SetLines:
                    command = new AdvCommandSetLines(row);
                    break;

                case enumCustomCommand.AddTweenColorLines:
                    command = new AdvCommandAddTweenColorLines(row);
                    break;

                case enumCustomCommand.AddTweenPositionLines:
                    command = new AdvCommandAddTweenPositionLines(row);

                    break;

                case enumCustomCommand.PlayTweenLines:
                    command = new AdvCommandPlayTweenLines(row);
                    break;

                case enumCustomCommand.SetArrows:
                    command = new AdvCommandSetArrows(row);
                    break;

                case enumCustomCommand.AddTweenPositionArrows:
                    command = new AdvCommandAddTweenPositionArrows(row);
                    break;

                case enumCustomCommand.AddTweenColorArrows:
                    command = new AdvCommandAddTweenColorArrows(row);
                    break;

                case enumCustomCommand.PlayTweenArrows:
                    command = new AdvCommandPlayTweenArrows(row);
                    break;

                case enumCustomCommand.SetRect:
                    command = new AdvCommandSetRect(row);
                    break;

                case enumCustomCommand.AddTweenColorRect:
                    command = new AdvCommandAddTweenColorRect(row);
                    break;

                case enumCustomCommand.AddTweenPositionRect:
                    command = new AdvCommandAddTweenPositionRect(row);
                    break;

                case enumCustomCommand.PlayTweenRect:
                    command = new AdvCommandPlayTweenRect(row);
                    break;

                case enumCustomCommand.ClearAll:
                    command = new AdvCommandClearAll(row);
                    Debug.Log(enumCustomCommand.ClearAll);
                    break;
            }
        }        
    }
}

/// <summary>선을 만든다</summary>
public class AdvCommandSetLines : AdvCommand
{
    public AdvCommandSetLines(StringGridRow row) : base(row)
    {
        //Debug.Log(CustomCommand.ins.lineController.dataSet.color32);
    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.lineController.dataSet.id = ParseCell<string>(AdvColumnName.Arg1);
        CustomCommand.ins.lineController.dataSet.listVec2.Add(CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg2)));
        CustomCommand.ins.lineController.dataSet.listVec2.Add(CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg3)));
        CustomCommand.ins.lineController.dataSet.color32 = CustomCommand.ins.StringToColor(ParseCell<string>(AdvColumnName.Arg4));

        CustomCommand.ins.lineController.SetLines(
            CustomCommand.ins.lineController.dataSet.id,
            CustomCommand.ins.lineController.dataSet.listVec2,
            CustomCommand.ins.lineController.dataSet.color32);
    }    
}

/// <summary>트윈 애니메이션을 만든다. Position전용</summary>
public class AdvCommandAddTweenPositionLines : AdvCommand
{
    public AdvCommandAddTweenPositionLines(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.lineController.AddTweenPositionLines(
            ParseCell<string>(AdvColumnName.Arg1),
            new List<Vector2> { CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg2)), CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg3))},
            ParseCell<float>(AdvColumnName.Arg6)
            );
    }
}

/// <summary>트윈 애니메이션을 만든다. Position전용</summary>
public class AdvCommandAddTweenColorLines : AdvCommand
{
    public AdvCommandAddTweenColorLines(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.lineController.AddTweenColorLines(
            ParseCell<string>(AdvColumnName.Arg1),
            CustomCommand.ins.StringToColor(ParseCell<string>(AdvColumnName.Arg4)),
            ParseCell<float>(AdvColumnName.Arg6)
            );
    }
}



/// <summary>트윈 애니메이션 재생</summary>
public class AdvCommandPlayTweenLines : AdvCommand
{
    public AdvCommandPlayTweenLines(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.lineController.PlayTweenLines(
            ParseCell<string>(AdvColumnName.Arg1)
            );
    }
}



/// <summary> 모든 선을 지운다. </summary>
public class AdvCommandClearAll : AdvCommand
{
    public AdvCommandClearAll(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.lineController.ClearAllLine();
        CustomCommand.ins.arrowController.ClearAllArrows();
    }
}


public class AdvCommandSetArrows : AdvCommand
{
    public AdvCommandSetArrows(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.arrowController.SetArrow(
             ParseCell<string>(AdvColumnName.Arg1),
             CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg2)),
             ParseCell<float>(AdvColumnName.Arg3),
             CustomCommand.ins.StringToColor(ParseCell<string>(AdvColumnName.Arg4)));
    }
}


public class AdvCommandAddTweenPositionArrows : AdvCommand
{
    public AdvCommandAddTweenPositionArrows(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.arrowController.AddTweenPositionArrows(ParseCell<string>(AdvColumnName.Arg1),
            CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg2)),
            ParseCell<float>(AdvColumnName.Arg3),
            ParseCell<float>(AdvColumnName.Arg6)
            );
    }
}


public class AdvCommandAddTweenColorArrows : AdvCommand
{
    public AdvCommandAddTweenColorArrows(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.arrowController.AddTweenColorArrows(ParseCell<string>(AdvColumnName.Arg1),
            CustomCommand.ins.StringToColor(ParseCell<string>(AdvColumnName.Arg4)),
            ParseCell<float>(AdvColumnName.Arg6)
            );
    }
}

public class AdvCommandPlayTweenArrows : AdvCommand
{
    public AdvCommandPlayTweenArrows(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.arrowController.PlayTweenArrows(ParseCell<string>(AdvColumnName.Arg1));
    }
}



/// <summary>
/// /
/// 
/// </summary>


public class AdvCommandSetRect : AdvCommand
{
    public AdvCommandSetRect(StringGridRow row) : base(row)
    {
    }

    public override void DoCommand(AdvEngine engine)
    {

        CustomCommand.ins.rectController.dataSet.id = ParseCell<string>(AdvColumnName.Arg1);
        CustomCommand.ins.rectController.dataSet.listVec2.Add(CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg2)));
        CustomCommand.ins.rectController.dataSet.listVec2.Add(CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg3)));
        CustomCommand.ins.rectController.dataSet.color32 = CustomCommand.ins.StringToColor(ParseCell<string>(AdvColumnName.Arg4));

        CustomCommand.ins.rectController.SetRect(
            CustomCommand.ins.rectController.dataSet.id,
            CustomCommand.ins.rectController.dataSet.listVec2,
            CustomCommand.ins.rectController.dataSet.color32);

    }
}


public class AdvCommandAddTweenPositionRect : AdvCommand
{
    public AdvCommandAddTweenPositionRect(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.rectController.AddTweenPositionLines(
            ParseCell<string>(AdvColumnName.Arg1),
            new List<Vector2> { CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg2)), CustomCommand.ins.StringToVec2(ParseCell<string>(AdvColumnName.Arg3)) },
            ParseCell<float>(AdvColumnName.Arg6)
        );
    }
}


public class AdvCommandAddTweenColorRect : AdvCommand
{
    public AdvCommandAddTweenColorRect(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.rectController.AddTweenColorLines(
            ParseCell<string>(AdvColumnName.Arg1),
            CustomCommand.ins.StringToColor(ParseCell<string>(AdvColumnName.Arg4)),
            ParseCell<float>(AdvColumnName.Arg6)
            );
    }
}

public class AdvCommandPlayTweenRect : AdvCommand
{
    public AdvCommandPlayTweenRect(StringGridRow row) : base(row)
    {

    }

    public override void DoCommand(AdvEngine engine)
    {
        CustomCommand.ins.rectController.PlayTweenLines(
            ParseCell<string>(AdvColumnName.Arg1)
            );
    }
}


