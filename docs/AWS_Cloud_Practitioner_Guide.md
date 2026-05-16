# AWS Certified Cloud Practitioner (CLF-C02) — Full Understanding Guide

The AWS Certified Cloud Practitioner is AWS's foundational/beginner certification. It validates a broad understanding of the AWS Cloud — independent of any specific technical role. There are **no prerequisites**. It is the right starting point for anyone new to AWS: students, sales, finance, project managers, and engineers who want a structured introduction.

This guide covers every domain of the current exam (CLF-C02), explains the *why* behind each concept, and gives you the vocabulary and decision rules examiners look for.

---

## Table of Contents

1. [Exam Overview](#1-exam-overview)
2. [Domain 1 — Cloud Concepts (24%)](#2-domain-1--cloud-concepts-24)
3. [Domain 2 — Security and Compliance (30%)](#3-domain-2--security-and-compliance-30)
4. [Domain 3 — Cloud Technology and Services (34%)](#4-domain-3--cloud-technology-and-services-34)
5. [Domain 4 — Billing, Pricing, and Support (12%)](#5-domain-4--billing-pricing-and-support-12)
6. [Cross-Cutting Service Cheat Sheet](#6-cross-cutting-service-cheat-sheet)
7. [Decision Patterns Examiners Love](#7-decision-patterns-examiners-love)
8. [Study Plan & Resources](#8-study-plan--resources)
9. [Exam-Day Strategy](#9-exam-day-strategy)

---

## 1. Exam Overview

| Item | Detail |
|---|---|
| Code | CLF-C02 (current as of 2024+) |
| Questions | 65 (50 scored + 15 unscored) |
| Time | 90 minutes |
| Format | Multiple choice / multiple response |
| Passing score | 700 / 1000 (scaled) |
| Delivery | Pearson VUE testing center or online proctored |
| Cost | USD $100 |
| Validity | 3 years |
| Languages | English, Japanese, Korean, Simplified Chinese, and others |

**Domain weighting:**

1. Cloud Concepts — **24%**
2. Security and Compliance — **30%**
3. Cloud Technology and Services — **34%**
4. Billing, Pricing, and Support — **12%**

You are *not* expected to write code, design architectures, or configure services. You *are* expected to know what each major service does, when to use it, and the AWS philosophy (Well-Architected pillars, shared responsibility, pricing models).

---

## 2. Domain 1 — Cloud Concepts (24%)

### 2.1 What is Cloud Computing?

Cloud computing is the **on-demand delivery of IT resources over the internet with pay-as-you-go pricing**. Instead of buying and maintaining physical servers, you rent capacity from a provider (AWS) and only pay for what you use.

The **Six Advantages of Cloud** (memorize — frequent exam material):

1. **Trade capital expense (CapEx) for variable expense (OpEx).** No upfront hardware spend.
2. **Benefit from massive economies of scale.** AWS aggregates demand, lowering per-unit cost.
3. **Stop guessing capacity.** Scale up or down as actual demand changes.
4. **Increase speed and agility.** Provision resources in minutes, not weeks.
5. **Stop spending money running and maintaining data centers.** Focus on differentiating work.
6. **Go global in minutes.** Deploy to multiple AWS Regions worldwide with a few clicks.

### 2.2 Cloud Deployment Models

- **Cloud (Public Cloud):** Fully in AWS. Most common.
- **Hybrid:** Mix of on-premises and cloud. Connected via VPN or AWS Direct Connect. Used during migrations or for workloads with data-residency constraints.
- **On-premises / Private Cloud:** Virtualized resources in your own data center (e.g., using VMware Cloud on AWS or AWS Outposts to get an AWS-style experience on-prem).

### 2.3 Cloud Service Models

| Model | You manage | Provider manages | Example |
|---|---|---|---|
| **IaaS** (Infrastructure) | OS, runtime, app, data | Hardware, virtualization, networking | Amazon EC2 |
| **PaaS** (Platform) | App, data | OS, runtime, scaling | AWS Elastic Beanstalk, RDS |
| **SaaS** (Software) | Just usage / config | Everything else | Amazon WorkMail, Microsoft 365 |

Rule of thumb: as you move IaaS → PaaS → SaaS, you give up control but reduce operational burden.

### 2.4 The AWS Global Infrastructure

Hierarchy: **Regions → Availability Zones (AZs) → Data Centers → Edge Locations.**

- **Region:** A geographical area (e.g., `us-east-1` = N. Virginia). Each Region is **isolated** from others for fault tolerance and data sovereignty. You choose a Region based on (1) compliance / data residency, (2) latency to users, (3) service availability, and (4) cost.
- **Availability Zone (AZ):** One or more discrete data centers within a Region, with redundant power, networking, and connectivity. AZs are physically separated (miles apart) but linked by low-latency fiber. A Region typically has **3+ AZs**. Design for *Multi-AZ* to survive a data-center failure.
- **Edge Locations / Points of Presence (PoPs):** Hundreds of sites worldwide used by **Amazon CloudFront** (CDN), **Route 53** (DNS), and **AWS Global Accelerator** to cache content close to end users.
- **Local Zones / Wavelength / Outposts:** Extensions of AWS for ultra-low-latency, 5G, or on-premises workloads.

### 2.5 The AWS Well-Architected Framework

A set of best-practice guidance built around **6 pillars**:

1. **Operational Excellence** — Run and monitor systems; continuously improve processes (e.g., Infrastructure as Code, runbooks).
2. **Security** — Protect data, systems, and assets (identity, detective controls, data protection, incident response).
3. **Reliability** — Recover from failure; meet demand; automatic recovery.
4. **Performance Efficiency** — Use computing resources efficiently; experiment more often (right-sizing, serverless).
5. **Cost Optimization** — Avoid unnecessary cost (right pricing model, right size, eliminate waste).
6. **Sustainability** — Minimize environmental impact (added in 2021).

The **AWS Well-Architected Tool** (free in the console) lets you review workloads against these pillars.

### 2.6 Core Design Principles

- **Design for failure.** Anything can fail; build for redundancy across AZs and Regions.
- **Decouple components.** Use queues (SQS), streams (Kinesis), and load balancers to reduce blast radius.
- **Implement elasticity.** Auto-scale horizontally rather than vertically when possible.
- **Think parallel.** Use stateless compute and managed services.
- **Loose coupling > tight coupling.** Microservices, event-driven design.

---

## 3. Domain 2 — Security and Compliance (30%)

### 3.1 The AWS Shared Responsibility Model

The single most-tested concept on the exam.

- **AWS is responsible for security *OF* the cloud:** Physical data centers, hardware, host OS and hypervisor, networking infrastructure, and managed-service software (e.g., the database engine running RDS).
- **The customer is responsible for security *IN* the cloud:** Operating system patches on EC2, application code, IAM users and policies, network and firewall configuration (security groups, NACLs), customer data, and encryption settings.

For **managed services**, AWS takes on more (e.g., RDS patches the DB engine). For **EC2**, the customer patches the guest OS.

| Always AWS | Always Customer | Shared |
|---|---|---|
| Physical security, hardware, hypervisor, global network | Customer data, IAM identities, in-guest configuration | Patching (depends on service), config, awareness/training |

### 3.2 AWS Identity and Access Management (IAM)

**IAM** controls *who* can do *what* on *which* AWS resource. IAM is **global** (not Region-specific) and **free**.

Key entities:

- **Root user** — Created when the AWS account is opened. Has full power. **Never use for daily tasks**, enable **MFA**, lock away credentials.
- **IAM User** — A person or app with long-term credentials (username/password and/or access keys).
- **IAM Group** — A collection of users; policies attach to the group.
- **IAM Role** — A temporary identity assumed by users, services, or external accounts. Roles deliver short-lived credentials via **AWS STS** — ideal for EC2 instances calling other services and for cross-account access.
- **IAM Policy** — JSON document granting/denying permissions. Two main flavors:
  - **Identity-based policies** attach to users, groups, or roles.
  - **Resource-based policies** attach to resources (e.g., S3 bucket policy).

**Permissions evaluation:**

1. **Default: implicit deny.**
2. **Explicit allow** in any applicable policy grants access.
3. **Explicit deny** always wins.

**Best practices (heavily tested):**

- Enable **MFA** for the root user and all privileged users.
- Apply the **principle of least privilege**.
- Use **roles** instead of long-lived access keys whenever possible (especially on EC2).
- Rotate credentials regularly.
- Use **IAM Identity Center** (formerly AWS SSO) for centralized workforce sign-in across accounts.

### 3.3 AWS Organizations

A service for managing **multiple AWS accounts** centrally.

- **Consolidated billing** — One invoice; share Reserved Instance and Savings Plan discounts across accounts; combine usage for volume discounts.
- **Service Control Policies (SCPs)** — Account-wide *guardrails* attached to **Organizational Units (OUs)** or accounts. SCPs **limit the maximum permissions** for IAM identities; they do not grant permissions.
- **AWS Control Tower** — Opinionated landing zone that sets up multi-account environments with best practices.

### 3.4 Compliance & Governance

- **AWS Artifact** — Self-service portal for compliance reports (SOC 1/2/3, ISO, PCI DSS, etc.) and agreements (BAA, NDA).
- **AWS Compliance Programs** — HIPAA, GDPR, FedRAMP, PCI DSS, ISO 27001, etc.
- **AWS Audit Manager** — Continuous audit-evidence collection.

### 3.5 Detective / Monitoring Services

| Service | Purpose |
|---|---|
| **AWS CloudTrail** | Records API calls (who did what, when, from where). On by default for management events. *Governance / audit.* |
| **Amazon CloudWatch** | Metrics, logs, alarms, dashboards. *Operational monitoring.* |
| **AWS Config** | Tracks resource configuration changes; lets you assert compliance rules ("all S3 buckets must be encrypted"). |
| **Amazon GuardDuty** | Intelligent threat detection (ML on CloudTrail, VPC Flow Logs, DNS logs). |
| **Amazon Inspector** | Vulnerability scanning of EC2, Lambda, and container images. |
| **AWS Security Hub** | Aggregated security findings across accounts and AWS services. |
| **Amazon Macie** | Discovers and protects sensitive data (PII) in S3. |
| **AWS Trusted Advisor** | Best-practice checks across cost, performance, security, fault tolerance, service limits, and operational excellence. |

> Mnemonic: **CloudTrail = who did it.** **CloudWatch = how is it performing.** **Config = how is it configured.**

### 3.6 Data Protection

- **Encryption in transit** — TLS/HTTPS.
- **Encryption at rest** — Managed by **AWS Key Management Service (KMS)** using **Customer Master Keys (CMKs/KMS keys)**. Integrated with S3, EBS, RDS, Lambda, and many others.
- **AWS CloudHSM** — Dedicated hardware security module for stricter compliance (FIPS 140-2 Level 3).
- **AWS Certificate Manager (ACM)** — Free public TLS/SSL certificates for use with ELB, CloudFront, API Gateway.
- **AWS Secrets Manager** — Stores and rotates secrets (DB passwords, API keys).
- **AWS Systems Manager Parameter Store** — Lower-cost alternative for configuration and secrets.

### 3.7 Network Security

- **Security Groups** — *Stateful* virtual firewalls attached to EC2/ENIs. Only allow rules. Return traffic automatically permitted.
- **Network ACLs (NACLs)** — *Stateless* subnet-level firewalls. Allow *and* deny rules. Return traffic must be explicitly allowed.
- **AWS WAF** — Layer-7 web firewall for CloudFront, ALB, API Gateway, and AppSync. Protects against SQL injection, XSS, bots.
- **AWS Shield (Standard / Advanced)** — DDoS protection. Standard is free and automatic; Advanced is paid with 24×7 support and cost-protection.
- **AWS Firewall Manager** — Centrally manage WAF/Shield/Security Groups across accounts.

---

## 4. Domain 3 — Cloud Technology and Services (34%)

You need broad recognition of services, not deep configuration knowledge.

### 4.1 Ways to Interact with AWS

- **AWS Management Console** — Web UI.
- **AWS CLI** — Command line interface.
- **AWS SDKs** — Language-specific libraries (Python `boto3`, .NET, Java, JS, etc.).
- **Infrastructure as Code:** **AWS CloudFormation** (declarative JSON/YAML templates) and **AWS CDK** (define infra in real programming languages).

### 4.2 Compute

| Service | What it is | When to use |
|---|---|---|
| **Amazon EC2** | Virtual machines (Linux/Windows) with full OS control. | Lift-and-shift, custom apps, full control. |
| **EC2 Auto Scaling** | Adds/removes EC2 instances based on demand. | Elasticity for stateless apps. |
| **Elastic Load Balancing (ELB)** | Distributes traffic across targets. Types: **ALB** (HTTP/HTTPS, L7), **NLB** (TCP/UDP, L4, ultra-low latency), **GWLB** (firewall appliances), Classic (legacy). | Make EC2/containers/Lambda highly available. |
| **AWS Lambda** | Serverless functions. Pay per invocation and ms of duration. Max 15-min runtime. | Event-driven code, lightweight APIs, glue logic. |
| **Amazon ECS** | Managed Docker container orchestration. | Containers without managing Kubernetes. |
| **Amazon EKS** | Managed Kubernetes. | Kubernetes-native workloads. |
| **AWS Fargate** | Serverless compute engine for ECS/EKS — no EC2 to manage. | Containers without server admin. |
| **AWS Elastic Beanstalk** | PaaS that handles capacity, scaling, load balancing for your code. Upload code, AWS runs it. | Quick deployment without managing infra. |
| **AWS Lightsail** | Simple, predictable-price VPS with bundled storage, networking, DNS. | Small workloads, learners, simple websites. |
| **AWS Batch** | Batch job scheduling on EC2/Fargate. | HPC, scientific compute, render farms. |
| **AWS Outposts** | AWS hardware in your data center. | On-prem with cloud APIs. |
| **AWS Wavelength / Local Zones** | Compute close to 5G / metro areas. | Ultra-low-latency edge apps. |

#### EC2 Purchasing Options (frequent exam topic)

| Option | Use case | Discount vs On-Demand |
|---|---|---|
| **On-Demand** | Short-term, unpredictable workloads. | 0% (baseline) |
| **Reserved Instances (RI)** — 1 or 3 years, Standard/Convertible | Steady-state workloads. | Up to ~72% |
| **Savings Plans** — 1 or 3 years, commit $/hour | Flexible commitment across EC2/Fargate/Lambda. | Up to ~72% |
| **Spot Instances** | Fault-tolerant, interruptible workloads (batch, CI, big data). Can be reclaimed with 2-min warning. | Up to ~90% |
| **Dedicated Hosts** | Compliance / per-socket licensing (BYOL). | Most expensive |
| **Dedicated Instances** | Hardware isolation without host-level visibility. | Premium |
| **Capacity Reservations** | Reserve capacity in an AZ without a price commitment. | None directly |

### 4.3 Storage

| Service | Type | Key facts |
|---|---|---|
| **Amazon S3** | Object storage | Unlimited scale, 11 nines (99.999999999%) of durability, accessed via HTTP(S) API. Stored in **buckets**; names are globally unique. |
| **S3 Storage Classes** | Tiers | Standard, Intelligent-Tiering, Standard-IA, One Zone-IA, Glacier Instant Retrieval, Glacier Flexible Retrieval, Glacier Deep Archive. **Lifecycle policies** transition objects automatically. |
| **Amazon EBS** | Block storage | Network-attached disks for EC2. Lives in one AZ. Snapshots stored in S3 and can be copied across Regions. |
| **EC2 Instance Store** | Block (ephemeral) | Physically attached; data lost on stop/terminate. Highest IOPS for temp data. |
| **Amazon EFS** | Shared file system (NFS) | Linux, multi-AZ, scales automatically. |
| **Amazon FSx** | Managed file systems | Variants: **FSx for Windows File Server** (SMB), **FSx for Lustre** (HPC), **FSx for NetApp ONTAP**, **FSx for OpenZFS**. |
| **AWS Storage Gateway** | Hybrid storage | Bridges on-prem apps to S3/EBS/Glacier. Modes: File, Volume, Tape. |
| **AWS Snow Family** | Physical data transfer | **Snowcone** (8 TB), **Snowball Edge** (up to ~80 TB + compute), **Snowmobile** (100 PB truck — being deprecated). Use when network transfer is too slow or expensive. |
| **AWS Backup** | Centralized backup | Manages backups across EBS, RDS, DynamoDB, EFS, FSx, Storage Gateway, etc. |

### 4.4 Databases

| Service | Type | Notes |
|---|---|---|
| **Amazon RDS** | Managed relational (MySQL, PostgreSQL, MariaDB, Oracle, SQL Server, Aurora) | Automated backups, patching, Multi-AZ for HA, read replicas for scale. |
| **Amazon Aurora** | AWS-built relational (MySQL- and PostgreSQL-compatible) | Up to 5× MySQL / 3× PostgreSQL performance, storage auto-scales to 128 TB, 6 copies across 3 AZs. **Aurora Serverless** auto-scales capacity. |
| **Amazon DynamoDB** | NoSQL key-value & document | Serverless, single-digit-millisecond latency, multi-Region with **Global Tables**, **DAX** for in-memory cache. |
| **Amazon ElastiCache** | In-memory cache | **Redis** or **Memcached**. Great for session stores and DB caching. |
| **Amazon MemoryDB for Redis** | Durable in-memory DB | Redis API with multi-AZ durability. |
| **Amazon Redshift** | Data warehouse (OLAP) | Petabyte-scale columnar; **Redshift Spectrum** queries S3 directly. |
| **Amazon Neptune** | Graph database | Social, fraud, knowledge graphs. |
| **Amazon DocumentDB** | MongoDB-compatible document DB | |
| **Amazon Keyspaces** | Apache Cassandra-compatible | |
| **Amazon Timestream** | Time-series database | IoT, ops monitoring. |
| **Amazon QLDB** | Ledger database | Immutable, cryptographically verifiable history. |
| **AWS Database Migration Service (DMS)** | Migrate DBs to AWS (homogeneous or heterogeneous; pair with **AWS SCT**). | |

### 4.5 Networking & Content Delivery

| Service | Purpose |
|---|---|
| **Amazon VPC** | Isolated virtual network; you control IP ranges, subnets, routing. Subnets are public or private. |
| **Subnets** | Live in a single AZ. *Public* has a route to an **Internet Gateway**; *private* doesn't. |
| **Internet Gateway / NAT Gateway** | IGW = bi-directional internet. NAT GW = lets private subnets reach internet outbound only. |
| **Route Tables** | Direct subnet traffic. |
| **VPC Peering** | Connect two VPCs privately. Non-transitive. |
| **AWS Transit Gateway** | Hub-and-spoke connecting many VPCs and on-prem networks. |
| **AWS PrivateLink / VPC Endpoints** | Reach AWS services or 3rd-party SaaS privately without going over the internet. *Gateway endpoints* exist for S3 and DynamoDB; *Interface endpoints* (powered by PrivateLink) for most other services. |
| **AWS Site-to-Site VPN** | Encrypted tunnel from on-prem to VPC over the internet. |
| **AWS Direct Connect** | Dedicated private fiber from on-prem to AWS. Lower latency, consistent throughput, often cheaper at scale. |
| **Amazon Route 53** | Authoritative DNS + domain registration + health checks. Routing policies: simple, weighted, latency, failover, geolocation, geoproximity, multi-value. |
| **Amazon CloudFront** | Global CDN built on edge locations; integrates with S3, ALB, custom origins; supports Lambda@Edge / CloudFront Functions. |
| **AWS Global Accelerator** | Uses AWS backbone + anycast IPs to improve performance and failover for global apps. |

### 4.6 Application Integration

| Service | Purpose |
|---|---|
| **Amazon SQS** | Fully managed message **queue** (point-to-point, pull). Standard or FIFO. Decouples producers and consumers. |
| **Amazon SNS** | **Pub/sub** topic (push) to many subscribers: Lambda, SQS, HTTPS, email, SMS, mobile push. |
| **Amazon EventBridge** | Serverless event bus; routes events from AWS services, SaaS, custom apps based on rules. Successor to CloudWatch Events. |
| **AWS Step Functions** | Visual workflow orchestrator that strings together Lambda and other services with state, retries, parallelism. |
| **Amazon MQ** | Managed message broker for ActiveMQ / RabbitMQ — useful when migrating existing apps. |
| **Amazon AppFlow** | No-code data integration between SaaS apps and AWS. |
| **Amazon API Gateway** | Create, publish, secure REST/HTTP/WebSocket APIs at scale. |

### 4.7 Analytics, ML, and Other Notable Services

- **Amazon Athena** — Serverless SQL queries over S3 data.
- **Amazon EMR** — Managed Hadoop/Spark/Presto clusters.
- **Amazon Kinesis** — Real-time streaming (Data Streams, Data Firehose, Data Analytics, Video Streams).
- **AWS Glue** — Serverless ETL and data catalog.
- **Amazon QuickSight** — BI dashboards.
- **AWS Lake Formation** — Build secure data lakes.
- **Amazon OpenSearch Service** — Managed OpenSearch / Elasticsearch.
- **AI/ML high-level services:** **Rekognition** (vision), **Comprehend** (NLP), **Polly** (text-to-speech), **Transcribe** (speech-to-text), **Translate**, **Lex** (chatbots), **Personalize** (recs), **Forecast**, **Textract**.
- **Amazon SageMaker** — End-to-end ML platform.
- **Amazon Bedrock** — Foundation models / generative AI as a service.
- **Amazon Q** — AWS's generative AI assistant for builders and business users.

### 4.8 Developer & Migration Tools

- **AWS CloudFormation** — Infrastructure as Code (JSON/YAML).
- **AWS CDK** — IaC in Python/TS/Java/Go/C#.
- **AWS CodeCommit / CodeBuild / CodeDeploy / CodePipeline / CodeArtifact** — Managed CI/CD building blocks.
- **AWS X-Ray** — Distributed tracing.
- **AWS Cloud9** — Browser IDE (being deprecated for new customers; mention only if asked).
- **AWS Application Migration Service (MGN)** — Lift-and-shift servers to AWS.
- **AWS Migration Hub** — Track migrations across tools.
- **AWS Schema Conversion Tool (SCT)** — Convert DB schemas during heterogeneous migrations.

---

## 5. Domain 4 — Billing, Pricing, and Support (12%)

### 5.1 Fundamental Pricing Principles

AWS pricing follows three core ideas:

1. **Pay-as-you-go** — Pay only for what you use, no upfront commitment required.
2. **Save when you reserve / commit** — Reserved Instances and Savings Plans give big discounts for 1- or 3-year commitments.
3. **Pay less by using more** — Tiered pricing (e.g., S3, data transfer) means per-unit prices drop at higher volumes.

**Always free:** IAM, AWS Organizations, AWS Cost Explorer (UI), VPC (itself), CloudFormation (templates), Auto Scaling.

**Free Tier** has three categories:

- **Always Free** — e.g., 1 M Lambda requests / month, 25 GB DynamoDB.
- **12-Months Free** — e.g., 750 hours of t2/t3.micro EC2 per month, 5 GB S3 Standard.
- **Trials** — Short-term free credits for specific services.

### 5.2 What You Pay For (mental model)

For nearly every service you pay for some combination of:

- **Compute** (per-second/hour or per-request).
- **Storage** (per GB-month).
- **Data transfer** — **Inbound is free**; outbound to the internet is charged (cross-Region and cross-AZ data also has costs). Within the same AZ and using private IPs is generally free.
- **Requests / operations** (e.g., S3 PUT/GET counts).

### 5.3 Billing & Cost-Management Tools

| Tool | What it does |
|---|---|
| **AWS Billing Console** | Invoices, payment methods, tax settings. |
| **AWS Cost Explorer** | Visualize and forecast spend by service, tag, account, etc. |
| **AWS Budgets** | Set cost/usage/RI/SP budgets with alerts (email, SNS). |
| **AWS Cost & Usage Report (CUR)** | Most detailed billing data; delivered to S3 for BI/Athena/QuickSight analysis. |
| **AWS Pricing Calculator** | Estimate cost of a proposed architecture before deploying. |
| **AWS Cost Anomaly Detection** | ML-based detection of unusual spend. |
| **Savings Plans / RI Recommendations** | Generated from your historical usage to maximize discount. |
| **AWS Trusted Advisor** | Includes cost-optimization checks (idle resources, low-utilization EC2, unassociated EIPs). |
| **Cost Allocation Tags** | Activate tags so they show up in reports; the foundation of chargeback/showback. |

### 5.4 AWS Organizations and Consolidated Billing

Benefits:

- **Single invoice** across all accounts.
- **Volume discounts** apply to combined usage.
- **Reserved Instance / Savings Plan sharing** across accounts (can be turned off per account).
- Centralized **SCPs** and account governance.

### 5.5 AWS Support Plans

This table is heavily tested — memorize it.

| Plan | Price (USD) | Tech Support | Trusted Advisor | Technical Account Manager (TAM) | Use case |
|---|---|---|---|---|---|
| **Basic** | Free | None (forums, docs, account & billing only) | 7 core checks | No | Everyone gets it. |
| **Developer** | From $29/mo | Business-hours email, 1 contact | 7 core checks | No | Test/dev environments. |
| **Business** | From $100/mo (3% of bill, tiered) | 24×7 phone/chat/email, unlimited contacts, 1-hour response for production-down | **Full** checks | No | Production workloads. |
| **Enterprise On-Ramp** | From $5,500/mo | 24×7, 30-min response for business-critical | Full | Pool of TAMs | Growing prod usage. |
| **Enterprise** | From $15,000/mo | 24×7, **15-min** response for business-critical | Full | **Designated TAM**, Concierge billing, IEM (Infrastructure Event Management), well-architected reviews | Large enterprises, mission-critical. |

Quick rules:
- Need a **TAM**? → **Enterprise On-Ramp** (pooled) or **Enterprise** (dedicated).
- Need **24×7 phone support**? → **Business** or higher.
- Need **15-minute response** on business-critical? → **Enterprise** only.
- Plan price is **per-account**, but with Organizations you can elect the support plan on the management account and use it across linked accounts via AWS support automation patterns.

### 5.6 Other Concierge / Professional Services

- **AWS Professional Services** — Paid AWS consulting team.
- **AWS Partner Network (APN)** — Consulting and Technology partners.
- **AWS IQ** — Marketplace for hiring AWS-certified experts on demand.
- **AWS Managed Services (AMS)** — AWS operates your AWS environment for you.
- **AWS re:Post** — Q&A community (successor to AWS Forums).
- **AWS Knowledge Center, Documentation, Whitepapers, Marketplace.**

---

## 6. Cross-Cutting Service Cheat Sheet

Compact "what is it in one line" reference:

- **EC2** — Virtual machines.
- **Lambda** — Serverless functions (≤15 min, event-driven).
- **S3** — Object storage with 11 9s durability.
- **EBS** — Disk volumes for EC2 (single AZ).
- **EFS** — Shared NFS file system (multi-AZ Linux).
- **FSx** — Managed Windows / Lustre / NetApp / ZFS file systems.
- **RDS** — Managed relational DBs.
- **Aurora** — Cloud-native, high-performance MySQL/PostgreSQL.
- **DynamoDB** — Serverless NoSQL key-value.
- **Redshift** — Petabyte-scale data warehouse.
- **VPC** — Your private network in AWS.
- **Route 53** — DNS + domain registrar + health checks.
- **CloudFront** — Global CDN.
- **API Gateway** — Managed REST/HTTP/WebSocket APIs.
- **SQS** — Pull-based message queue (decouple).
- **SNS** — Push pub/sub topic (fan-out).
- **EventBridge** — Event bus with rules.
- **Step Functions** — Workflow orchestrator.
- **IAM** — Identity & permissions (global, free).
- **KMS** — Managed encryption keys.
- **CloudTrail** — Records API activity.
- **CloudWatch** — Metrics, logs, alarms.
- **Config** — Resource configuration history & compliance rules.
- **GuardDuty** — Threat detection.
- **Inspector** — Vulnerability scans.
- **Macie** — PII discovery in S3.
- **Security Hub** — Aggregated security findings.
- **WAF** — Web application firewall (L7).
- **Shield** — DDoS protection (Standard free, Advanced paid).
- **Trusted Advisor** — Best-practice checks.
- **Organizations** — Multi-account management + consolidated billing.
- **Control Tower** — Multi-account landing zone.
- **CloudFormation** — IaC templates.
- **Elastic Beanstalk** — PaaS for app deployment.
- **Lightsail** — Simple VPS.
- **ECS / EKS / Fargate** — Containers (Docker / K8s / serverless).
- **Snow Family** — Physical data transport.
- **Storage Gateway** — Hybrid on-prem ↔ AWS storage.
- **Direct Connect** — Dedicated network from data center to AWS.
- **Site-to-Site VPN** — IPSec tunnel over the internet to a VPC.
- **Global Accelerator** — Anycast IPs over the AWS backbone for global apps.
- **SageMaker** — Build/train/deploy ML.
- **Bedrock** — Foundation-model API for generative AI.

---

## 7. Decision Patterns Examiners Love

Memorize these "if you see X, the answer is usually Y" patterns:

- "Pay only for what you use" → **Cloud / On-Demand / Serverless**.
- "Don't manage servers / no infrastructure" → **Lambda, Fargate, S3, DynamoDB, Aurora Serverless, Athena**.
- "Highly available" → **Multi-AZ**.
- "Disaster recovery / lowest possible RTO across regions" → **Multi-Region**.
- "Cheapest storage for archive, retrieval in hours" → **S3 Glacier Deep Archive**.
- "Decouple components" → **SQS** (or SNS for fan-out).
- "Real-time streaming data" → **Kinesis**.
- "Run SQL on data already in S3 without loading" → **Amazon Athena**.
- "Petabyte-scale BI warehouse" → **Redshift**.
- "Block storage for one EC2 instance" → **EBS**.
- "Shared file system across many EC2 (Linux)" → **EFS**.
- "Move 100 TB of data, network too slow" → **AWS Snowball**.
- "Centralized billing across accounts" → **AWS Organizations**.
- "Audit who did what in my account" → **CloudTrail**.
- "Alert me when CPU > 80%" → **CloudWatch alarm**.
- "Detect threats automatically" → **GuardDuty**.
- "Find PII in S3" → **Macie**.
- "Centralized security findings" → **Security Hub**.
- "DDoS protection with cost guarantees + 24/7 DDoS response team" → **Shield Advanced**.
- "Block SQL injection at the edge" → **AWS WAF**.
- "Free SSL/TLS certs for ELB or CloudFront" → **ACM**.
- "Need a Technical Account Manager (TAM)" → **Enterprise On-Ramp or Enterprise** support.
- "Production workload, 24×7 support, less than $15k/month" → **Business** support.
- "Cheapest EC2 for fault-tolerant batch jobs" → **Spot Instances**.
- "Steady-state workload for 3 years" → **Savings Plan** (or Reserved Instance).
- "Compliance reports (SOC 2, ISO)" → **AWS Artifact**.
- "Hybrid storage cache to on-prem" → **AWS Storage Gateway**.
- "Connect on-prem to AWS with dedicated, predictable bandwidth" → **AWS Direct Connect**.
- "Encrypted tunnel quickly, over internet" → **Site-to-Site VPN**.
- "DNS with traffic routing policies and health checks" → **Route 53**.
- "Cache static and dynamic content globally" → **CloudFront**.

---

## 8. Study Plan & Resources

A focused plan (intensity, not calendar time):

1. **Pass 1 — Foundations.** Read this guide top to bottom. Watch AWS's free **Cloud Practitioner Essentials** course on **AWS Skill Builder**. Goal: understand the vocabulary.
2. **Pass 2 — Hands-on.** Create a free-tier AWS account. Practice:
   - Launch and connect to an EC2 instance; create a security group.
   - Create an S3 bucket; upload an object; set a lifecycle policy.
   - Create an IAM user, a group, attach a policy, and enable MFA.
   - Create a VPC with one public and one private subnet.
   - Deploy a Lambda function from the console; trigger it from S3 or EventBridge.
   - Use Cost Explorer and create an AWS Budget.
3. **Pass 3 — Deep on weak spots.** Re-read the domains where practice questions trip you up (usually Security and Billing).
4. **Pass 4 — Practice exams.** Take **at least 3 full practice exams**. Aim consistently ≥ 80%. The official AWS practice exam (free in Skill Builder) plus reputable third-party sets (Tutorials Dojo / Stéphane Maarek) are gold standard.
5. **Pass 5 — Review the AWS whitepapers** that map to the exam:
   - *Overview of Amazon Web Services*
   - *AWS Well-Architected Framework*
   - *How AWS Pricing Works*
   - *AWS Shared Responsibility Model*

Recommended free resources:

- AWS Skill Builder — *Cloud Practitioner Essentials* (free).
- AWS Documentation (per service "What is …?" pages).
- AWS Whitepapers & Guides.
- AWS re:Invent talks on YouTube (search "AWS 101").
- *AWS in Plain English* community articles.

---

## 9. Exam-Day Strategy

- **Read every word.** Watch for qualifiers like *most cost-effective*, *least operational overhead*, *highly available*, *real time*, *serverless*.
- **Eliminate first.** Two of the four options are usually obviously wrong; pick the *most AWS-recommended* of the remaining two.
- **Flag and move on.** Don't spend > 90 seconds on a single question on the first pass.
- **Trust the "AWS opinion."** If an answer recommends serverless / managed services / multi-AZ / least privilege / IaC / Well-Architected pillars, it is usually correct.
- **Beware made-up answers.** AWS sometimes inserts plausible-sounding non-services (e.g., "Amazon SafeDeploy"). If you've never heard of it, suspect it.
- **Time check at Q30 and Q50.** You have ~80 seconds per question; you should have at least 30 minutes left when you finish the first pass.

---

### TL;DR — What You Must Know Cold

- The **6 advantages of cloud**.
- **Regions vs AZs vs Edge Locations** and why you'd pick each.
- **Shared Responsibility Model** — examples on both sides.
- **IAM** users/groups/roles/policies, root account best practices, MFA, least privilege.
- The **Well-Architected Framework's 6 pillars**.
- **EC2 purchasing options** (On-Demand, Reserved, Savings Plans, Spot, Dedicated).
- **S3 storage classes** and when to use Glacier tiers.
- **EBS vs EFS vs FSx vs Instance Store vs S3.**
- **RDS vs Aurora vs DynamoDB vs Redshift.**
- **VPC building blocks** and **Security Groups vs NACLs** (stateful vs stateless).
- **CloudTrail vs CloudWatch vs Config**.
- **AWS Organizations** + Consolidated Billing + SCPs.
- The **AWS Support plans** matrix.
- The **Billing tools** (Cost Explorer, Budgets, Pricing Calculator, CUR, Trusted Advisor).
- The **Snow Family**, **Direct Connect**, and **Storage Gateway** for hybrid/migration scenarios.

Master the items above and you will not only pass the CLF-C02 — you will have the mental model needed to advance to the Associate-level certifications (Solutions Architect, Developer, SysOps).

Good luck.
