using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReactiveUI;
using System.Reactive;
using System;

namespace Ava2;

public class MyModel : ReactiveObject
{
    string fpn = "Калькулятор алхимика";
    public string FirstPageName
    {
        get => fpn;
        set => this.RaiseAndSetIfChanged(ref fpn, value);
    }

    string cwh = "20";
    public string СoncentrationWeHave
    {
        get => cwh;
        set => this.RaiseAndSetIfChanged(ref cwh, value);
    }

    string cwn = "5";
    public string СoncentrationWeNeed
    {
        get => cwn;
        set => this.RaiseAndSetIfChanged(ref cwn, value);
    }

    string vwn = "500";
    public string VolumeWeNeed
    {
        get => vwn;
        set => this.RaiseAndSetIfChanged(ref vwn, value);
    }

    string wv;
    public string WaterVolume
    {
        get => wv;
        set => this.RaiseAndSetIfChanged(ref wv, value);
    }

    string sv;
    public string SolutionVolume
    {
        get => sv;
        set => this.RaiseAndSetIfChanged(ref sv, value);
    }

    public ReactiveCommand<Unit, Unit> CalculateCommand { get; }
    public void DoCalculateOLD()
    {
        var cwh = double.Parse(СoncentrationWeHave);
        if (cwh < 0)
        {
            cwh = 1;
            СoncentrationWeHave = cwh.ToString();
        }
        var cwn = double.Parse(СoncentrationWeNeed);
        if (cwn < 0)
        {
            cwn = 1;
            СoncentrationWeNeed = cwn.ToString();
        }
        var vwn = double.Parse(VolumeWeNeed);
        if (vwn < 0)
        {
            vwn = 1;
            VolumeWeNeed = vwn.ToString();
        }
        var wv = (vwn * cwn) / (cwh - cwn);
        var sv = vwn - wv;
        WaterVolume = wv.ToString();
        SolutionVolume = sv.ToString();
    }

    // rewrite DoCalculate() using TryParse()
    public void DoCalculate()
    {
        if (double.TryParse(СoncentrationWeHave, out var cwh))
        {
            if (cwh < 0)
            {
                cwh = 1;
                СoncentrationWeHave = cwh.ToString();
            }
        }
        else
        {
            cwh = 1;
            СoncentrationWeHave = cwh.ToString();
        }

        if (double.TryParse(СoncentrationWeNeed, out var cwn))
        {
            if (cwn < 0)
            {
                cwn = 1;
                СoncentrationWeNeed = cwn.ToString();
            }
        }
        else
        {
            cwn = 1;
            СoncentrationWeNeed = cwn.ToString();
        }

        if (double.TryParse(VolumeWeNeed, out var vwn))
        {
            if (vwn < 0)
            {
                vwn = 1;
                VolumeWeNeed = vwn.ToString();
            }
        }
        else
        {
            vwn = 1;
            VolumeWeNeed = vwn.ToString();
        }

        
        var wv = vwn / cwh * cwn;
        var sv = vwn / cwh * (cwh - cwn);
        WaterVolume = wv.ToString();
        SolutionVolume = sv.ToString();

        (WaterVolume, SolutionVolume) = (SolutionVolume, WaterVolume);

        
    }




 
    public MyModel()
    {
        CalculateCommand = ReactiveCommand.Create(DoCalculate);

        CalculateCommand.Subscribe(_ => DoCalculate());

        // calculate when any of the properties change
        this.WhenAnyValue(x => x.СoncentrationWeHave, x => x.СoncentrationWeNeed, x => x.VolumeWeNeed)
            .Subscribe(_ => DoCalculate());





        // DoCalculate();




        


        
    }
}
