using System;
using System.Collections.Generic;
using Entitas;

public class GameEventSystem : ReactiveSystem<GlobalEntity>, ICleanupSystem {
  private readonly Action<string[]> _uiMessageRenderer;
  private Queue<string> log = new Queue<string>();
  private IGroup<GlobalEntity> _messages;
  private const int MAX_MESSAGES = 9;

  public GameEventSystem(Contexts contexts, Action<string[]> uiMessageRenderer) : base(contexts.global) {
    _uiMessageRenderer = uiMessageRenderer;
    _messages = contexts.global.GetGroup(GlobalMatcher.MessageLog);
  }

  protected override ICollector<GlobalEntity> GetTrigger(IContext<GlobalEntity> context) {
    return context.CreateCollector(GlobalMatcher.MessageLog);
  }

  protected override bool Filter(GlobalEntity entity) {
    return entity.hasMessageLog;
  }

  protected override void Execute(List<GlobalEntity> entities) {
    foreach (var e in entities) {
      var m = e.messageLog.message;
      log.Enqueue(m);
      if (log.Count > MAX_MESSAGES)
        log.Dequeue();
    }

    _uiMessageRenderer(log.ToArray());
  }

  public void Cleanup() {
    foreach (var message in _messages.GetEntities()) {
      message.Destroy();
    }
  }
}