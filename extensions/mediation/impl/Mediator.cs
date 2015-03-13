/*
 * Copyright 2013 ThirdMotion, Inc.
 *
 *	Licensed under the Apache License, Version 2.0 (the "License");
 *	you may not use this file except in compliance with the License.
 *	You may obtain a copy of the License at
 *
 *		http://www.apache.org/licenses/LICENSE-2.0
 *
 *		Unless required by applicable law or agreed to in writing, software
 *		distributed under the License is distributed on an "AS IS" BASIS,
 *		WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *		See the License for the specific language governing permissions and
 *		limitations under the License.
 */

/**
 * @class strange.extensions.mediation.impl.Mediator
 * 
 * Base class for all Mediators.
 * 
 * @see strange.extensions.mediation.api.IMediationBinder
 */

using mastermind.core.util;
using strange.extensions.context.api;
using strange.extensions.mediation.api;
using UnityEngine;

namespace strange.extensions.mediation.impl
{
	public class Mediator : MonoBehaviour, IMediator
	{

		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{get;set;}

        private bool _isActivated;
	    private bool _isRegistered;
	    
		public Mediator ()
		{
		}

		/**
		 * Fires directly after creation and before injection
		 */
		virtual public void PreRegister()
		{
		}

		/**
		 * Fires after all injections satisifed.
		 *
		 * Override and place your initialization code here.
		 */
		virtual public void OnRegister()
		{
		    _isRegistered = true;

            TryActivate();
		}

		/**
		 * Fires on removal of view.
		 *
		 * Override and place your cleanup code here
		 */
		virtual public void OnRemove()
		{
            TryDeactivate();
		}

	    protected virtual void OnActivate()
	    {
	    }

        protected virtual void OnDeactivate()
        {
        }

        protected virtual void OnEnable()
        {
            TryActivate();
        }

        protected void OnDisable()
        {
            TryDeactivate();
        }

	    private void TryActivate()
	    {
            if (gameObject != null && gameObject.activeInHierarchy && !_isActivated && _isRegistered && !Application.isLoadingLevel)
            {
                Logger.Error("calling activate: " + this);
                OnActivate();
                _isActivated = true;
            }
	    }

	    private void TryDeactivate()
	    {
            if (_isRegistered && _isActivated)
            {
                Logger.Error("calling deactivate: " + this);
                OnDeactivate();
                _isActivated = false;
            }
	    }
	}
}

