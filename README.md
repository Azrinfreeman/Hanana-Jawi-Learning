# Belajar Jawi

A Unity Jawi-learning prototype aimed at preschool and primary-school learners. The project combines question/answer activities, timed challenges, health and score feedback, and animal rewards with local player profiles.

## Project highlights

- Learning scenes grouped into Part 1–4, with additional zoo/reward scenes.
- Preschool answer selection and separate answer-arrangement controllers.
- Configured countdown, correct/incorrect audio, health depletion, and win/failure displays.
- Saved rewards and animal unlocks keyed to scene or object names and the current player index.
- Local profile selection and an existing Hanana score-reporting integration.

These features are described from source inspection. This review does not establish that all scenes are fully wired, lesson content is correct, or the game works on its intended devices.

## Open the project

1. Install **Unity 2022.3.60f1**, matching [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt), and clone the repository with its assets.
2. Add the repository root through Unity Hub and allow asset import and package resolution to finish.
3. Open [SplashScreen](Assets/Scenes/SplashScreen/SplashScreen.unity). Review networking before entering Play mode with player data, and use synthetic profiles for development checks.
4. Navigate through FrontMenu and exercise the learning and reward scenes.

Check the enabled scene list in [build settings](ProjectSettings/EditorBuildSettings.asset). The [manifest](Packages/manifest.json) includes TextMesh Pro 3.0.9, Sprite Shape 9.0.5, and Visual Scripting 1.9.8. Compilation and package compatibility were not verified by this documentation-only change.

## Code guide

| Area | Entry point |
| --- | --- |
| Preschool question selection | [QuestionPrasekolahController](Assets/QuestionPrasekolahController.cs) |
| Preschool answer flow | [JawapanPrasekolahController](Assets/JawapanPrasekolahController.cs) |
| Answer-arrangement flow | [JawapanSusunanController](Assets/JawapanSusunanController.cs) |
| Health loss | [HealthController](Assets/HealthController.cs) |
| Scene/player reward restoration | [RewardController2](Assets/RewardController2.cs) |
| Animal unlock visibility | [PetUnlockerController](Assets/PetUnlockerController.cs) |
| Profile setup and reporting | [GameStartupController](Assets/GameStartupController.cs) |

## Saved data and backend

PlayerPrefs stores local profiles, scores, and reward flags. Several unlock keys are derived from scene/object names plus the current player index; renaming those objects can affect saved progress.

GameStartupController contains a POST to `app-hanana.com/jawimodul1/insertmark.php`, sending player name, player ID, score/reward values, and device name. Server source is not included. The service was not contacted during this review; availability, authorization, and data synchronization remain unverified. Inspect networking before demonstrations with real learner data.

## Status and reuse

See [validation notes](docs/VALIDATION.md) for source observations and manual checks. This update changes documentation only; it preserves scripts, scenes, assets, build/debug artifacts, crash reports, and saved data. No new license is assigned; review bundled asset and audio permissions before redistribution.
