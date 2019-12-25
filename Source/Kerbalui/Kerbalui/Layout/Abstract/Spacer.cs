using System;
using System.Collections.Generic;
using System.Linq;
using Kerbalui.Controls.Abstract;
using Kerbalui.Types;
using UnityEngine;

namespace Kerbalui.Layout.Abstract
{
    /// <summary>
    /// Contains common functionality for the Vertical and Horizontal Spacers.
    /// </summary>
    public abstract class Spacer : Group
    {
        protected List<SpacerEntry> spacerEntries = new List<SpacerEntry>();

        /// <summary>
        /// Adds a spacer entry of one of 3 types for controlling spacing.
        /// WEIGHTED: The elements will be assigned space proportional to their weight.
        /// FIXED: The elements will be reserved space equal to the size specified.
        /// MINSIZED: The elements will be reserved space equal to their minimum content size.
        /// </summary>
        void Add(SpacerEntry spacerEntry)
        {
            spacerEntries.Add(spacerEntry);
			RegisterForUpdate(spacerEntry.element);
			needsResize = true;
			PostAddElement();
        }
        public void AddWeighted(float weight, Element element)
        {
            Add(new SpacerEntry(weight, element, SpacerEntryType.WEIGHTED));
        }
        public void AddFixed(float size, Element element)
        {
            Add(new SpacerEntry(size, element, SpacerEntryType.FIXED));
        }
        public void AddMinSized(Element element)
        {
            Add(new SpacerEntry(0, element, SpacerEntryType.MINSIZED));
        }

        /// <summary>
        /// Subclasses can override this to specify additional behavior occuring after an "Add", for example,
        /// to trigger a resize on a parent component.
        /// </summary>
        protected virtual void PostAddElement()
        {

        }

        protected float MinContentWidth(SpacerEntry spacerEntry)
        {
            switch (spacerEntry.type)
            {
                case SpacerEntryType.FIXED:
                    return spacerEntry.size*KerbaluiSettings.UI_SCALE;
                case SpacerEntryType.MINSIZED:
                    return spacerEntry.element.MinSize.x;
            }
            return 0;
        }

        protected float MinContentHeight(SpacerEntry spacerEntry)
        {
            switch (spacerEntry.type)
            {
                case SpacerEntryType.FIXED:
					return spacerEntry.size*KerbaluiSettings.UI_SCALE;
                case SpacerEntryType.MINSIZED:
                    return spacerEntry.element.MinSize.y;
            }
            return 0;
        }

        protected List<float> CalculateSpacingPoints(Func<SpacerEntry, float> getMinSize, float totalSize)
        {
            var spacingPoints = new List<float> { 0 };
            float totalWeight = 0;

            float totalMinContentSize = 0;
            foreach (var spacerEntry in spacerEntries)
            {
                // Ignore spacing for elements that are not active
                if (!spacerEntry.element.Active)
                {
                    continue;
                }

                if (spacerEntry.type == SpacerEntryType.WEIGHTED)
                {
                    totalWeight += spacerEntry.size;
                }
                else
                {
                    totalMinContentSize += getMinSize(spacerEntry);
                }
            }

            float minfract = totalMinContentSize / totalSize;

            float totalWeightFract = 1 - minfract;

            // Multiplying weightMultiplier to spacerEntry.weight, gives us the width for that entry
            // spacerEntryWidth=spacerEntry.weight*weightMultiplier
            // weightMultiplier=1/totalWeight*totalWeightFract*rect.width
            float weightMultiplier = totalWeightFract / totalWeight * totalSize;

            float minContentSize = 0;
            float startPoint = 0;
            float endPoint = 0;
            foreach (var spacerEntry in spacerEntries)
            {
                // If the element is not active we will just use the same start and endpoint.
                if (spacerEntry.element.Active)
                {
                    minContentSize = getMinSize(spacerEntry);
                    if (spacerEntry.type==SpacerEntryType.WEIGHTED)
                    {
						endPoint = startPoint + spacerEntry.size * weightMultiplier;
					}
                    else
                    {
						endPoint = startPoint + minContentSize;
                    }
                }

				//Debug.Log("endpoint is "+endPoint);
				spacingPoints.Add(endPoint);
                startPoint = endPoint;
            }

            return spacingPoints;
        }

        protected enum SpacerEntryType
        {
            MINSIZED,
            WEIGHTED,
            FIXED,
        }

        protected struct SpacerEntry
        {
            /// <summary>
            /// The type enum determines whether 'size' is treated as a fixed size or weight
            /// </summary>
            public float size;
            public Element element;
            public SpacerEntryType type;

            public SpacerEntry(float size, Element element, SpacerEntryType type)
            {
                this.size = size;
                this.element = element;
                this.type = type;
            }
        }
    }
}
