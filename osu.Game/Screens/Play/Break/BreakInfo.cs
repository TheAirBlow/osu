// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.LocalisationExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Localisation;
using osu.Game.Resources.Localisation.Web;
using osu.Game.Scoring;
using osuTK;

namespace osu.Game.Screens.Play.Break
{
    public partial class BreakInfo : Container
    {
        public PercentageBreakInfoLine AccuracyDisplay;

        // Currently unused but may be revisited in a future design update (see https://github.com/ppy/osu/discussions/15185)
        // public BreakInfoLine<int> RankDisplay;
        public BreakInfoLine<ScoreRank> GradeDisplay;

        private readonly FillFlowContainer linesContainer;
        private readonly Bindable<bool> anxietyMode = new Bindable<bool>();

        public BreakInfo()
        {
            AutoSizeAxes = Axes.Both;
            Child = new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(5),
                Children = new Drawable[]
                {
                    new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Text = BreakInfoStrings.CurrentProgressTitle.ToUpper(),
                        Font = OsuFont.GetFont(weight: FontWeight.Bold, size: 15),
                    },
                    linesContainer = new FillFlowContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Direction = FillDirection.Vertical,
                        Children = new Drawable[]
                        {
                            AccuracyDisplay = new PercentageBreakInfoLine(BeatmapsetsStrings.ShowScoreboardHeadersAccuracy),
                            // See https://github.com/ppy/osu/discussions/15185
                            // RankDisplay = new BreakInfoLine<int>("Rank"),
                            GradeDisplay = new BreakInfoLine<ScoreRank>(BreakInfoStrings.ShowInfoGrade),
                        },
                    }
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            config.BindWith(OsuSetting.AnxietyMode, anxietyMode);
            anxietyMode.BindValueChanged(m => linesContainer.Alpha = m.NewValue ? 0 : 1, true);
        }
    }
}
