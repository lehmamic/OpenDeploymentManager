using System;
using System.Activities;
using OpenDeploymentManager.Deployment.Activities.Common;

namespace OpenDeploymentManager.Deployment.Activities
{
    public static class TrackingExtensions
    {
        public static void TrackDeploymentError(this ActivityContext context, string message)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            IActivityTracking activityTracking = context.GetExtension<IDeploymentLoggingExtension>().GetActivityTracking(context);
            activityTracking.Node.Children.AddErrorNode(message);
        }

        public static void TrackDeploymentMessage(this ActivityContext context, string message)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            IActivityTracking activityTracking = context.GetExtension<IDeploymentLoggingExtension>().GetActivityTracking(context);
            activityTracking.Node.Children.AddInfoNode(message);
        }

        public static void TrackDeploymentWarning(this ActivityContext context, string message)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            IActivityTracking activityTracking = context.GetExtension<IDeploymentLoggingExtension>().GetActivityTracking(context);
            activityTracking.Node.Children.AddWarningNode(message);
        }

        public static IDeploymentInformationNode AddErrorNode(this IDeploymentInformationNodeCollection collection, string message)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            return collection.CreateNode("Error", message);
        }

        public static IDeploymentInformationNode AddInfoNode(this IDeploymentInformationNodeCollection collection, string message)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            return collection.CreateNode("Info", message);
        }

        public static IDeploymentInformationNode AddWarningNode(this IDeploymentInformationNodeCollection collection, string message)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            return collection.CreateNode("Warning", message);
        }
    }
}