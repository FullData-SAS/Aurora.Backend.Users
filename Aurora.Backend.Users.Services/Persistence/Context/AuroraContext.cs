using Aurora.Backend.Users.Services.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
namespace Aurora.Backend.Users.Services.Persistence.Context;

public partial class AuroraContext : DbContext
{
    public AuroraContext()
    {
    }

    public AuroraContext(DbContextOptions<AuroraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<CertifiedEmail> CertifiedEmails { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DigitalSignature> DigitalSignatures { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentAction> DocumentActions { get; set; }

    public virtual DbSet<DocumentFile> DocumentFiles { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Entity> Entities { get; set; }

    public virtual DbSet<FileRecord> FileRecords { get; set; }

    public virtual DbSet<Filing> Filings { get; set; }

    public virtual DbSet<FilingDocumentType> FilingDocumentTypes { get; set; }

    public virtual DbSet<FilingFile> FilingFiles { get; set; }

    public virtual DbSet<Flow> Flows { get; set; }

    public virtual DbSet<GroupPermission> GroupPermissions { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<LabelComponent> LabelComponents { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<RetentionTable> RetentionTables { get; set; }

    public virtual DbSet<RetentionTableDocumentType> RetentionTableDocumentTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<Step> Steps { get; set; }

    public virtual DbSet<StepUser> StepUsers { get; set; }

    public virtual DbSet<Subclient> Subclients { get; set; }

    public virtual DbSet<Subseries> Subseries { get; set; }

    public virtual DbSet<UserDigitalSignature> UserDigitalSignatures { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Host=aurora-prod.cmxcgkc8m3lc.us-east-1.rds.amazonaws.com;Port=5432;Database=Aurora;Username=leandro;Password=Leandro2025*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("sgdea", "company_type", new[] { "private", "public", "mixed" })
            .HasPostgresEnum("sgdea", "document_status", new[] { "draft", "filed" })
            .HasPostgresEnum("sgdea", "file_status", new[] { "active", "inactive" })
            .HasPostgresEnum("sgdea", "flow_step_type", new[] { "approval", "review", "notification" })
            .HasPostgresEnum("sgdea", "label_component_type", new[] { "barcode", "filingNumber", "date", "text" })
            .HasPostgresEnum("sgdea", "label_type", new[] { "filing", "file", "box" })
            .HasPostgresEnum("sgdea", "medium_type", new[] { "physical", "digital", "hybrid" })
            .HasPostgresEnum("sgdea", "person_type", new[] { "natural", "legal" })
            .HasPostgresEnum("sgdea", "physical_procedure", new[] { "scanned-destroyed", "scanned-preserved" })
            .HasPostgresEnum("sgdea", "time_unit", new[] { "days", "months", "years" });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("app_user_pkey");

            entity.ToTable("app_user", "sgdea", tb => tb.HasComment("System users"));

            entity.HasIndex(e => e.Email, "app_user_email_key").IsUnique();

            entity.HasIndex(e => new { e.DocumentType, e.DocumentNumber }, "idx_user_document");

            entity.HasIndex(e => e.Email, "idx_user_email");

            entity.HasIndex(e => e.GroupId, "idx_user_group");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(20)
                .HasColumnName("document_number");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(10)
                .HasColumnName("document_type");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.LastLogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_login");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Group).WithMany(p => p.AppUsers)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("app_user_group_id_fkey");
        });

        modelBuilder.Entity<CertifiedEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("certified_email_pkey");

            entity.ToTable("certified_email", "sgdea", tb => tb.HasComment("Certified emails acquired by subclients"));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AvailableEmails)
                .HasComputedColumnSql("(total_emails - used_emails)", true)
                .HasColumnName("available_emails");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(50)
                .HasColumnName("order_number");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.SubclientId).HasColumnName("subclient_id");
            entity.Property(e => e.TotalEmails).HasColumnName("total_emails");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UsedEmails)
                .HasDefaultValue(0)
                .HasColumnName("used_emails");
            entity.Property(e => e.Value)
                .HasPrecision(12, 2)
                .HasColumnName("value");

            entity.HasOne(d => d.Subclient).WithMany(p => p.CertifiedEmails)
                .HasForeignKey(d => d.SubclientId)
                .HasConstraintName("certified_email_subclient_id_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client", "sgdea", tb => tb.HasComment("Stores information about the main clients of the system"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CompanyType)
                .HasMaxLength(50)
                .HasColumnName("company_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TaxId)
                .HasMaxLength(20)
                .HasColumnName("tax_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contact_pkey");

            entity.ToTable("contact", "sgdea", tb => tb.HasComment("Contacts associated with subclients"));

            entity.HasIndex(e => e.SubclientId, "idx_contact_subclient");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("department");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasColumnName("position");
            entity.Property(e => e.SubclientId).HasColumnName("subclient_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Subclient).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.SubclientId)
                .HasConstraintName("contact_subclient_id_fkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("department_pkey");

            entity.ToTable("department", "sgdea", tb => tb.HasComment("Departments within entities"));

            entity.HasIndex(e => new { e.EntityId, e.Code }, "department_entity_id_code_key").IsUnique();

            entity.HasIndex(e => e.EntityId, "idx_department_entity");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Entity).WithMany(p => p.Departments)
                .HasForeignKey(d => d.EntityId)
                .HasConstraintName("department_entity_id_fkey");
        });

        modelBuilder.Entity<DigitalSignature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("digital_signature_pkey");

            entity.ToTable("digital_signature", "sgdea", tb => tb.HasComment("Digital signatures acquired by subclients"));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AvailableSeats)
                .HasComputedColumnSql("(total_seats - used_seats)", true)
                .HasColumnName("available_seats");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(50)
                .HasColumnName("order_number");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.SubclientId).HasColumnName("subclient_id");
            entity.Property(e => e.TotalSeats).HasColumnName("total_seats");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UsedSeats)
                .HasDefaultValue(0)
                .HasColumnName("used_seats");
            entity.Property(e => e.Value)
                .HasPrecision(12, 2)
                .HasColumnName("value");

            entity.HasOne(d => d.Subclient).WithMany(p => p.DigitalSignatures)
                .HasForeignKey(d => d.SubclientId)
                .HasConstraintName("digital_signature_subclient_id_fkey");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_pkey");

            entity.ToTable("document", "sgdea", tb => tb.HasComment("Documents generated in the system"));

            entity.HasIndex(e => e.Number, "document_number_key").IsUnique();

            entity.HasIndex(e => e.AssignedToId, "idx_document_assigned");

            entity.HasIndex(e => e.FileId, "idx_document_file");

            entity.HasIndex(e => e.Number, "idx_document_number");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AssignedToId).HasColumnName("assigned_to_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Entity)
                .HasMaxLength(100)
                .HasColumnName("entity");
            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .HasColumnName("number");
            entity.Property(e => e.Series)
                .HasMaxLength(100)
                .HasColumnName("series");
            entity.Property(e => e.Subseries)
                .HasMaxLength(100)
                .HasColumnName("subseries");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AssignedTo).WithMany(p => p.DocumentAssignedTos)
                .HasForeignKey(d => d.AssignedToId)
                .HasConstraintName("document_assigned_to_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DocumentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("document_created_by_fkey");

            entity.HasOne(d => d.File).WithMany(p => p.Documents)
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("document_file_id_fkey");
        });

        modelBuilder.Entity<DocumentAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_action_pkey");

            entity.ToTable("document_action", "sgdea");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .HasColumnName("action");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentActions)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("document_action_document_id_fkey");
        });

        modelBuilder.Entity<DocumentFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_file_pkey");

            entity.ToTable("document_file", "sgdea");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("url");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentFiles)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("document_file_document_id_fkey");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_type_pkey");

            entity.ToTable("document_type", "sgdea", tb => tb.HasComment("Types of documents that can be managed"));

            entity.HasIndex(e => e.Code, "document_type_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Entity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entity_pkey");

            entity.ToTable("entity", "sgdea", tb => tb.HasComment("Organizational entities"));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<FileRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("file_record_pkey");

            entity.ToTable("file_record", "sgdea", tb => tb.HasComment("Files that group filings"));

            entity.HasIndex(e => e.Number, "file_record_number_key").IsUnique();

            entity.HasIndex(e => e.Number, "idx_file_number");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(200)
                .HasColumnName("company_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(20)
                .HasColumnName("document_number");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(10)
                .HasColumnName("document_type");
            entity.Property(e => e.Entity)
                .HasMaxLength(100)
                .HasColumnName("entity");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .HasColumnName("number");
            entity.Property(e => e.Series)
                .HasMaxLength(100)
                .HasColumnName("series");
            entity.Property(e => e.Subseries)
                .HasMaxLength(100)
                .HasColumnName("subseries");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FileRecords)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("file_record_created_by_fkey");
        });

        modelBuilder.Entity<Filing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("filing_pkey");

            entity.ToTable("filing", "sgdea", tb => tb.HasComment("Filings that contain document types"));

            entity.HasIndex(e => e.Number, "filing_number_key").IsUnique();

            entity.HasIndex(e => e.FileId, "idx_filing_file");

            entity.HasIndex(e => e.Number, "idx_filing_number");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .HasColumnName("number");
            entity.Property(e => e.Subject)
                .HasMaxLength(200)
                .HasColumnName("subject");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.File).WithMany(p => p.Filings)
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("filing_file_id_fkey");
        });

        modelBuilder.Entity<FilingDocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("filing_document_type_pkey");

            entity.ToTable("filing_document_type", "sgdea");

            entity.HasIndex(e => new { e.FilingId, e.DocumentTypeId }, "filing_document_type_filing_id_document_type_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");
            entity.Property(e => e.FilingId).HasColumnName("filing_id");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.FilingDocumentTypes)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("filing_document_type_document_type_id_fkey");

            entity.HasOne(d => d.Filing).WithMany(p => p.FilingDocumentTypes)
                .HasForeignKey(d => d.FilingId)
                .HasConstraintName("filing_document_type_filing_id_fkey");
        });

        modelBuilder.Entity<FilingFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("filing_file_pkey");

            entity.ToTable("filing_file", "sgdea");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FilingDocumentTypeId).HasColumnName("filing_document_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("url");

            entity.HasOne(d => d.FilingDocumentType).WithMany(p => p.FilingFiles)
                .HasForeignKey(d => d.FilingDocumentTypeId)
                .HasConstraintName("filing_file_filing_document_type_id_fkey");
        });

        modelBuilder.Entity<Flow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("flow_pkey");

            entity.ToTable("flow", "sgdea", tb => tb.HasComment("Workflows for document processes"));

            entity.HasIndex(e => e.Code, "flow_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<GroupPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("group_permission_pkey");

            entity.ToTable("group_permission", "sgdea", tb => tb.HasComment("Permissions assigned to groups"));

            entity.HasIndex(e => new { e.GroupId, e.Module }, "group_permission_group_id_module_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AllocatePerm)
                .HasDefaultValue(false)
                .HasColumnName("allocate_perm");
            entity.Property(e => e.ApprovePerm)
                .HasDefaultValue(false)
                .HasColumnName("approve_perm");
            entity.Property(e => e.AssignPerm)
                .HasDefaultValue(false)
                .HasColumnName("assign_perm");
            entity.Property(e => e.CreatePerm)
                .HasDefaultValue(false)
                .HasColumnName("create_perm");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletePerm)
                .HasDefaultValue(false)
                .HasColumnName("delete_perm");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.ManagePerm)
                .HasDefaultValue(false)
                .HasColumnName("manage_perm");
            entity.Property(e => e.Module)
                .HasMaxLength(50)
                .HasColumnName("module");
            entity.Property(e => e.ReadPerm)
                .HasDefaultValue(false)
                .HasColumnName("read_perm");
            entity.Property(e => e.SignPerm)
                .HasDefaultValue(false)
                .HasColumnName("sign_perm");
            entity.Property(e => e.UpdatePerm)
                .HasDefaultValue(false)
                .HasColumnName("update_perm");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("group_permission_group_id_fkey");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("label_pkey");

            entity.ToTable("label", "sgdea", tb => tb.HasComment("Labels for printing"));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Width).HasColumnName("width");
        });

        modelBuilder.Entity<LabelComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("label_component_pkey");

            entity.ToTable("label_component", "sgdea");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FontFamily)
                .HasMaxLength(50)
                .HasColumnName("font_family");
            entity.Property(e => e.FontSize).HasColumnName("font_size");
            entity.Property(e => e.LabelId).HasColumnName("label_id");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("name_en");
            entity.Property(e => e.NameEs)
                .HasMaxLength(100)
                .HasColumnName("name_es");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .HasColumnName("value");

            entity.HasOne(d => d.Label).WithMany(p => p.LabelComponents)
                .HasForeignKey(d => d.LabelId)
                .HasConstraintName("label_component_label_id_fkey");
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("license_pkey");

            entity.ToTable("license", "sgdea", tb => tb.HasComment("Licenses acquired by subclients"));

            entity.HasIndex(e => e.SubclientId, "idx_license_subclient");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.AvailableSeats)
                .HasComputedColumnSql("(total_seats - used_seats)", true)
                .HasColumnName("available_seats");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.Module)
                .HasMaxLength(50)
                .HasColumnName("module");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(50)
                .HasColumnName("order_number");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.SubclientId).HasColumnName("subclient_id");
            entity.Property(e => e.TotalSeats).HasColumnName("total_seats");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UsedSeats)
                .HasDefaultValue(0)
                .HasColumnName("used_seats");
            entity.Property(e => e.Value)
                .HasPrecision(12, 2)
                .HasColumnName("value");

            entity.HasOne(d => d.Subclient).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.SubclientId)
                .HasConstraintName("license_subclient_id_fkey");
        });

        modelBuilder.Entity<RetentionTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("retention_table_pkey");

            entity.ToTable("retention_table", "sgdea", tb => tb.HasComment("Document retention tables"));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("department");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(20)
                .HasColumnName("department_code");
            entity.Property(e => e.HumanRights)
                .HasDefaultValue(false)
                .HasColumnName("human_rights");
            entity.Property(e => e.HumanitarianLaw)
                .HasDefaultValue(false)
                .HasColumnName("humanitarian_law");
            entity.Property(e => e.IsRequired)
                .HasDefaultValue(false)
                .HasColumnName("is_required");
            entity.Property(e => e.PhysicalRetentionCentral).HasColumnName("physical_retention_central");
            entity.Property(e => e.PhysicalRetentionManagement).HasColumnName("physical_retention_management");
            entity.Property(e => e.Series)
                .HasMaxLength(100)
                .HasColumnName("series");
            entity.Property(e => e.SeriesCode)
                .HasMaxLength(20)
                .HasColumnName("series_code");
            entity.Property(e => e.Subseries)
                .HasMaxLength(100)
                .HasColumnName("subseries");
            entity.Property(e => e.SubseriesCode)
                .HasMaxLength(20)
                .HasColumnName("subseries_code");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<RetentionTableDocumentType>(entity =>
        {
            entity.HasKey(e => new { e.RetentionTableId, e.DocumentTypeId }).HasName("retention_table_document_type_pkey");

            entity.ToTable("retention_table_document_type", "sgdea");

            entity.Property(e => e.RetentionTableId).HasColumnName("retention_table_id");
            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.RetentionTableDocumentTypes)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("retention_table_document_type_document_type_id_fkey");

            entity.HasOne(d => d.RetentionTable).WithMany(p => p.RetentionTableDocumentTypes)
                .HasForeignKey(d => d.RetentionTableId)
                .HasConstraintName("retention_table_document_type_retention_table_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role", "sgdea", tb => tb.HasComment("Roles that can be assigned to users"));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_permission_pkey");

            entity.ToTable("role_permission", "sgdea", tb => tb.HasComment("Permissions assigned to roles"));

            entity.HasIndex(e => new { e.RoleId, e.Module }, "role_permission_role_id_module_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatePerm)
                .HasDefaultValue(false)
                .HasColumnName("create_perm");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletePerm)
                .HasDefaultValue(false)
                .HasColumnName("delete_perm");
            entity.Property(e => e.Module)
                .HasMaxLength(50)
                .HasColumnName("module");
            entity.Property(e => e.ReadPerm)
                .HasDefaultValue(false)
                .HasColumnName("read_perm");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatePerm)
                .HasDefaultValue(false)
                .HasColumnName("update_perm");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("role_permission_role_id_fkey");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("series_pkey");

            entity.ToTable("series", "sgdea", tb => tb.HasComment("Document series"));

            entity.HasIndex(e => e.EntityId, "idx_series_entity");

            entity.HasIndex(e => new { e.EntityId, e.Code }, "series_entity_id_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Entity).WithMany(p => p.Series)
                .HasForeignKey(d => d.EntityId)
                .HasConstraintName("series_entity_id_fkey");
        });

        modelBuilder.Entity<Step>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("step_pkey");

            entity.ToTable("step", "sgdea", tb => tb.HasComment("Steps of workflows"));

            entity.HasIndex(e => e.FlowId, "idx_step_flow");

            entity.HasIndex(e => new { e.FlowId, e.OrderNum }, "step_flow_id_order_num_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FlowId).HasColumnName("flow_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.OrderNum).HasColumnName("order_num");
            entity.Property(e => e.Required)
                .HasDefaultValue(false)
                .HasColumnName("required");
            entity.Property(e => e.TimeLimit).HasColumnName("time_limit");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Flow).WithMany(p => p.Steps)
                .HasForeignKey(d => d.FlowId)
                .HasConstraintName("step_flow_id_fkey");
        });

        modelBuilder.Entity<StepUser>(entity =>
        {
            entity.HasKey(e => new { e.StepId, e.UserId }).HasName("step_user_pkey");

            entity.ToTable("step_user", "sgdea");

            entity.Property(e => e.StepId).HasColumnName("step_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Step).WithMany(p => p.StepUsers)
                .HasForeignKey(d => d.StepId)
                .HasConstraintName("step_user_step_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.StepUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("step_user_user_id_fkey");
        });

        modelBuilder.Entity<Subclient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subclient_pkey");

            entity.ToTable("subclient", "sgdea", tb => tb.HasComment("Stores information about subclients associated with a main client"));

            entity.HasIndex(e => e.ClientId, "idx_subclient_client");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TaxId)
                .HasMaxLength(20)
                .HasColumnName("tax_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Client).WithMany(p => p.Subclients)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("subclient_client_id_fkey");
        });

        modelBuilder.Entity<Subseries>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subseries_pkey");

            entity.ToTable("subseries", "sgdea", tb => tb.HasComment("Document subseries"));

            entity.HasIndex(e => e.SeriesId, "idx_subseries_series");

            entity.HasIndex(e => new { e.SeriesId, e.Code }, "subseries_series_id_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.SeriesId).HasColumnName("series_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Series).WithMany(p => p.Subseries)
                .HasForeignKey(d => d.SeriesId)
                .HasConstraintName("subseries_series_id_fkey");
        });

        modelBuilder.Entity<UserDigitalSignature>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.DigitalSignatureId }).HasName("user_digital_signature_pkey");

            entity.ToTable("user_digital_signature", "sgdea");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.DigitalSignatureId).HasColumnName("digital_signature_id");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("assigned_at");

            entity.HasOne(d => d.DigitalSignature).WithMany(p => p.UserDigitalSignatures)
                .HasForeignKey(d => d.DigitalSignatureId)
                .HasConstraintName("user_digital_signature_digital_signature_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserDigitalSignatures)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_digital_signature_user_id_fkey");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_group_pkey");

            entity.ToTable("user_group", "sgdea", tb => tb.HasComment("User groups with specific permissions"));

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });
        modelBuilder.HasSequence("document_seq", "sgdea");
        modelBuilder.HasSequence("file_seq", "sgdea");
        modelBuilder.HasSequence("filing_seq", "sgdea");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
