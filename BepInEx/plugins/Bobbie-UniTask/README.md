# UniTask 2.5.0

Provides an efficient allocation free async/await integration for Unity.

* Struct based `UniTask<T>` and custom AsyncMethodBuilder to achieve zero allocation
* Makes all Unity AsyncOperations and Coroutines awaitable
* PlayerLoop based task(`UniTask.Yield`, `UniTask.Delay`, `UniTask.DelayFrame`, etc..) that enable replacing all coroutine operations
* MonoBehaviour Message Events and uGUI Events as awaitable/async-enumerable
* Runs completely on Unity's PlayerLoop so doesn't use threads and runs on WebGL, wasm, etc.
* Asynchronous LINQ, with Channel and AsyncReactiveProperty
* TaskTracker window to prevent memory leaks
* Highly compatible behaviour with Task/ValueTask/IValueTaskSource

For technical details, see blog post: [UniTask v2 — Zero Allocation async/await for Unity, with Asynchronous LINQ
](https://medium.com/@neuecc/unitask-v2-zero-allocation-async-await-for-unity-with-asynchronous-linq-1aa9c96aa7dd)  
For advanced tips, see blog post: [Extends UnityWebRequest via async decorator pattern — Advanced Techniques of UniTask](https://medium.com/@neuecc/extends-unitywebrequest-via-async-decorator-pattern-advanced-techniques-of-unitask-ceff9c5ee846)

For usage details, check [the original README](https://github.com/Cysharp/UniTask).
## Licensing 
I did not make this library, I am simply uploading it so it can be used as a dependency in mods. The original repo (licensed under MIT) can be found [here](https://github.com/Cysharp/UniTask).

## For Developers

### **NOTE:** This is different than the UniTask package found on nuget!

The UniTask package on nuget is just for .NET, and doesn't contain many useful Unity-based utilities you may want.

If you'd like to use UniTask, I'd recommend referencing this package's DLLs directly.