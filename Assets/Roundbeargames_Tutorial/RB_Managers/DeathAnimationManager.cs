using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class DeathAnimationManager : Singleton<DeathAnimationManager>
    {
		DeathAnimationLoader deathAnimationLoader;
		List<RuntimeAnimatorController> candidates = new List<RuntimeAnimatorController>();

		void SetupDeathAnimationLoader()
		{
			if (deathAnimationLoader == null)
			{
				GameObject obj = Instantiate(Resources.Load("DeathAnimationLoader", typeof(GameObject)) as GameObject);
				deathAnimationLoader = obj.GetComponent<DeathAnimationLoader>();
			}
		}

        public RuntimeAnimatorController GetAnimator(GeneralBodyPart generalBodyPart, AttackInfo info)
        {
            SetupDeathAnimationLoader();

            candidates.Clear();

            foreach (var data in deathAnimationLoader.deathAnimationDataList)
            {
                if (data.deathType == info.deathType)
                {
                    if (info.deathType != DeathType.NONE)
                    {
                        candidates.Add(data.animator);
                    }
                    else if (!info.mustCollider)
                    {
                        candidates.Add(data.animator);
                    }
                    else
                    {
                        foreach (GeneralBodyPart part in data.generalBodyParts)
                        {
                            if (part == generalBodyPart)
                            {
                                candidates.Add(data.animator);
                                break;
                            }
                        }
                    }
                }
            }

            return candidates[Random.Range(0, candidates.Count)];
        }
	}
}
