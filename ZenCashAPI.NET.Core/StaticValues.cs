using System;
using System.Collections.Generic;
using System.Text;

namespace ZenCashAPI.NET.Core
{
    public enum ServerRegion
    {
        NONE, NA, EU
    }

    public enum ServerNumber
    {
        NONE, ONE, TWO, THREE, FOUR, FIVE, SIX
    }

    public enum NodeType
    {
        SECURE, SUPER
    }

    public enum NodeStatus
    {
        NONE, UP, DOWN
    }

    public enum DowntimeStatus
    {
        NONE, OPEN, CLOSED, EXCLUDE
    }

    public enum ExceptionStatus
    {
        NONE, OPEN, CLOSED, EXCLUDE
    }

    public enum ChallengeResult
    {
        NONE, PASS, FAIL
    }

    public enum PaymentStatus
    {
        NONE, EXCLUDE
    }
}
