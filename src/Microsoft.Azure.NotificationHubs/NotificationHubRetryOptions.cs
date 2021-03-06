﻿//------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved. 
// Licensed under the MIT License. See License.txt in the project root for 
// license information.
//------------------------------------------------------------

using System;

namespace Microsoft.Azure.NotificationHubs
{
    /// <summary>
    ///    The set of options that can be specified to influence how
    ///    retry attempts are made, and a failure is eligible to be retried.
    /// </summary>
    ///
    public class NotificationHubRetryOptions
    {
        /// <summary>The maximum number of retry attempts before considering the associated operation to have failed.</summary>
        private int _maxRetries = 3;

        /// <summary>The delay or back-off factor to apply between retry attempts.</summary>
        private TimeSpan _delay = TimeSpan.FromSeconds(1);

        /// <summary>The maximum delay to allow between retry attempts.</summary>
        private TimeSpan _maxDelay = TimeSpan.FromMinutes(1);

        /// <summary>
        ///   The approach to use for calculating retry delays.
        /// </summary>
        ///
        public NotificationHubRetryMode Mode { get; set; } = NotificationHubRetryMode.Exponential;

        /// <summary>
        ///   The maximum number of retry attempts before considering the associated operation
        ///   to have failed.
        /// </summary>
        ///
        public int MaxRetries
        {
            get => _maxRetries;

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("The maximum number of retry attempts has to be between 0 and 100", nameof(MaxRetries));
                }

                _maxRetries = value;
            }
        }

        /// <summary>
        ///   The delay between retry attempts for a fixed approach or the delay
        ///   on which to base calculations for a backoff-based approach.
        /// </summary>
        ///
        public TimeSpan Delay
        {
            get => _delay;

            set
            {
                if (value < TimeSpan.FromMilliseconds(1) || value > TimeSpan.FromMinutes(5))
                {
                    throw new ArgumentException("The delay between retry attempts has to be between 1 millisecond and 5 minutes", nameof(Delay));
                }

                _delay = value;
            }
        }

        /// <summary>
        ///   The maximum permissible delay between retry attempts.
        /// </summary>
        ///
        public TimeSpan MaxDelay
        {
            get => _maxDelay;

            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw new ArgumentException("The maximum permissible delay between retry attempts can not be negative", nameof(MaxDelay));
                }

                _maxDelay = value;
            }
        }

        /// <summary>
        ///   A custom retry policy to be used in place of the individual option values.
        /// </summary>
        ///
        /// <remarks>
        ///   When populated, this custom policy will take precedence over the individual retry
        ///   options provided.
        /// </remarks>
        ///
        public NotificationHubRetryPolicy CustomRetryPolicy { get; set; }
    }
}
