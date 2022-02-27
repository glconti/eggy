using System.Runtime.CompilerServices;

namespace Eggy.Client.Domain.System;

public static class TaskExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConfiguredTaskAwaitable NoContext(this Task task) =>
        task.ConfigureAwait(false);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConfiguredTaskAwaitable<TResult> NoContext<TResult>(this Task<TResult> task) =>
        task.ConfigureAwait(false);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConfiguredValueTaskAwaitable NoContext(this ValueTask task) =>
        task.ConfigureAwait(false);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConfiguredValueTaskAwaitable<TResult> NoContext<TResult>(
        this ValueTask<TResult> task) => task.ConfigureAwait(false);
}