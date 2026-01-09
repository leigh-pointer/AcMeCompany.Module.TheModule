# **Oqtane UI Construction Rules (Enforced)**

## Purpose

These rules define **mandatory UI construction standards*- for all Oqtane modules.

They exist to ensure:

- Consistent user experience across modules
- Predictable behavior for validation and actions
- Alignment with canonical Oqtane UI patterns
- Prevention of implicit or framework-invented behavior

[Rule 2: Prohibited Use of EditForm](#rule-2-prohibited-use-of-editform)
## Rule 2: Prohibited Use of `EditForm`

The Blazor `EditForm` component **must not be used**.

UI must be constructed using **explicit HTML elements**, including:

- `<form>`
- `<input>`
- `<select>`
- `<textarea>`

Validation and save behavior must be **explicit and visible*- in code.

**Reject if:**

- `EditForm` is used
- Validation logic is hidden behind framework abstractions
- Form submission behavior is implicit or inferred

---

## Rule 3: Explicit Button Type Declaration

All buttons **must explicitly declare their type**.

```
<button type="button">
```

**Reject if:**

- The `type` attribute is omitted
- Default browser behavior is relied upon
- Button intent is ambiguous

---

## Rule 4: Controlled Use of `type="submit"`

`type="submit"` **must not be used*- unless:

- The request explicitly requires submit semantics
- Submit behavior is intentional, reviewed, and documented

**Reject if:**

- `type="submit"` is used by default
- Submit is used as a convenience
- Submit behavior is implied rather than deliberate

---

## Rule 5: Navigation via Oqtane Mechanisms

Navigation **must*- use Oqtane-approved patterns:

- `ActionLink`
- Oqtane routing infrastructure

**Reject if:**

- Direct URL manipulation is used
- `NavigationManager.NavigateTo` is used where `ActionLink` is appropriate
- Anchor tags bypass Oqtane navigation conventions

---

## Canonical Alignment

These rules align with UI patterns found in:

- Oqtane framework admin modules
- HtmlText module
- Core Oqtane edit and management pages

The **Oqtane Framework*- itself is the canonical reference.

---

## Validation Checklist

A UI implementation is valid only if:

- Bootstrap is used exclusively
- Bootstrap Icons are used for icons
- `EditForm` is not present
- All buttons declare `type`
- `type="submit"` is only used when explicitly required
- Navigation uses `ActionLink` or equivalent Oqtane routing

If any check fails, **reject the change**.

---

## Enforcement

- This rule is **enforced**
- AI must refuse to generate or modify UI code that violates this document
- Violations must not be worked around or softened
- If uncertainty exists, refusal is preferred over assumption