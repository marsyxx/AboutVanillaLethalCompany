# v1.2.2

- Fixed the counter sometimes showing an empty label on the first scan of a session. It is built as
  an inactive copy of the game's scan display, and TextMeshPro does not generate its text mesh until
  the object is first enabled — so the frame it appeared on could draw the panel with no text on it.
  The text is now rebuilt the moment the counter is shown.

# v1.2

Unofficial update by **Dysora**, based on **tinyhoot's** original ShipLoot 1.1.0, with edits of its
own. See ATTRIBUTION.md.
