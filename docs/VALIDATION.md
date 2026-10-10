# Documentation review and manual checks

Baseline: `9d9a55b6646c23f0de6b83146b0a8aab4e179598`.

## Scope and evidence

README and these notes only. No scripts, gameplay, scenes, settings, packages, assets, outputs, or saved data changed.

Reviewed preschool question/answer structure, arrangement progression, timed/health fields, reward restoration, pet visibility, profile indexing, scene navigation, and score-reporting fields. Checked local documentation links, enabled scene paths, whitespace, and documentation-only scope.

No Unity compilation, gameplay, device build, or live service request was performed. This is not a full-history security or asset-rights audit.

## Source observations

- QuestionPrasekolahController gathers active answer children and indexes questions using randomNumber. Validate the three-item assumption, list initialization/reset, and empty or inconsistent scene content.
- HealthController assumes three configured children and, after a delay, indexes/removes the final health entry without an empty-list guard. Check overlapping wrong-answer/timeout events and repeated damage at zero health.
- JawapanSusunanController indexes child a and destroys child a minus one during progression. Check first/last-item boundaries and repeated Next actions.
- RewardController2 restores flags using sceneName, reward index, and current player index; pet flags use object names. Verify restart/profile changes and avoid changing names without a progress migration decision.
- DetailPlayer parses only the last character of a profile key as its index. Test ten or more profiles and deletion/selection paths.
- Several paths rely on named objects, fixed child positions, and singleton/startup order. Inspect every enabled learning/reward scene rather than inferring correctness from source alone.
- Local progress and external reporting are separate mechanisms. Verify the reported values and error behavior before claiming server synchronization.
- Tracked Burst debug output and crash artifacts are retained. Review distributable contents and asset/audio rights separately before release.

## Manual checklist

1. Import with Unity 2022.3.60f1; inspect compilation, package compatibility, references, tags, and build settings.
2. Navigate SplashScreen/FrontMenu and each enabled Part 1–4 and zoo scene; inspect serialized lessons and answers with a Jawi subject reviewer.
3. Test correct/wrong answers, arrangement steps, countdown expiry, repeated health loss, win/failure, and interruption/restart boundaries.
4. Use synthetic profiles in a reviewed networking setup. Check rewards/pets after reload, profile switching/deletion, and multi-digit indices.
5. Test intended controls/device builds, audio failures, reporting errors, and asset/vendor permissions before distribution.

Source observations above remain unfixed in this documentation-only change.
