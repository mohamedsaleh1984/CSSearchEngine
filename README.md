# SearchEngine in C#

Building a milestone for a search engine to replace Windows Search, aiming to avoid Windows slowness.
## Phase 1
## Table of Contents
- Tokenizer
- Indexing
- Levenshtein Distance Implementation
- Token Comparer (Sorting Helpers)
- Search Prompt

The project is designed to allow any component to be replaced with a different implementation.

## Phase 2
(Under development)
Build a service to Index all files in the background. Handle actions through FileSystem watchers
to track the location changes and avoid re-indexing.

## Phase 3
(Under development)
Build an Event Hook to read the input from Windows Explorer and pass this to a handler how
search the indexed space and show the result including
- filename
- size
- location
