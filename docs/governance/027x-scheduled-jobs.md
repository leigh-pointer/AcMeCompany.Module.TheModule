# 027x – Scheduled Job Rules

## Scope

Applies to all scheduled or background job execution.

---

## Core Execution Model

- Scheduled Jobs are framework-managed by Oqtane
- Jobs are not generic background services
- Jobs execute once per tenant per run
- Jobs must be safe to re-run

---

## Mandatory Requirements

- Must inherit `HostedServiceBase`
- Must reside in the Server project
- Must accept `IServiceScopeFactory` in the constructor
- Must define scheduling metadata via public properties

---

## Execution Rules

- Implement `ExecuteJob` or `ExecuteJobAsync`
- Resolve dependencies from provided `IServiceProvider`
- Be short-lived and non-blocking
- Be tenant-safe and idempotent

---

## Forbidden Patterns

- `BackgroundService`
- `IHostedService`
- Timers, cron, Hangfire, Quartz
- Custom scheduling or lifecycle logic

---

## Rejection Criteria

Reject if:

- Any forbidden pattern is used
- Execution is long-running or stateful
- Tenant safety is violated
- Scheduling is implemented outside metadata
