﻿using System;
using EmergenceGuardian.OntraportApi.Converters;

namespace EmergenceGuardian.OntraportApi.Models
{
    /// <summary>
    /// Custom objects behave like contacts and have their own properties, forms, campaigns and emails.
    /// </summary>
    public class ApiCustomObjectBase : ApiObject
    {
        /// <summary>
        /// Returns a ApiProperty object to get or set the ID of the user who controls the custom object. This field must contain a value for a custom object to be saved properly.
        /// </summary>
        public ApiProperty<int> OwnerField => _ownerField ?? (_ownerField = new ApiProperty<int>(this, OwnerKey));
        private ApiProperty<int> _ownerField;
        public const string OwnerKey = "owner";
        /// <summary>
        /// Gets or sets the ID of the user who controls the custom object. This field must contain a value for a custom object to be saved properly.
        /// </summary>
        public int? Owner { get => OwnerField.Value; set => OwnerField.Value = value; }

        /// <summary>
        /// Returns a ApiProperty object to get or set the date the custom object was added.
        /// </summary>
        public ApiPropertyDateTime DateAddedField => _dateAddedField ?? (_dateAddedField = new ApiPropertyDateTime(this, DateAddedKey));
        private ApiPropertyDateTime _dateAddedField;
        public const string DateAddedKey = "date";
        /// <summary>
        /// Gets or sets the date the custom object was added.
        /// </summary>
        public DateTimeOffset? DateAdded { get => DateAddedField.Value; set => DateAddedField.Value = value; }

        /// <summary>
        /// Returns a ApiProperty object to get or set the date of the custom object's last activity. In this documentation, activity means that the object object interacted with a form, website, or email link.
        /// </summary>
        public ApiPropertyDateTime DateLastActivityField => _dateLastActivityField ?? (_dateLastActivityField = new ApiPropertyDateTime(this, DateLastActivityKey));
        private ApiPropertyDateTime _dateLastActivityField;
        public const string DateLastActivityKey = "dla";
        /// <summary>
        /// Gets or sets the date of the custom object's last activity. In this documentation, activity means that the object object interacted with a form, website, or email link.
        /// </summary>
        public DateTimeOffset? DateLastActivity { get => DateLastActivityField.Value; set => DateLastActivityField.Value = value; }

        /// <summary>
        /// Returns a ApiProperty object to get or set the date the custom object was last modified.
        /// </summary>
        public ApiPropertyDateTime DateLastModifiedField => _dateLastModifiedField ?? (_dateLastModifiedField = new ApiPropertyDateTime(this, DateLastModifiedKey));
        private ApiPropertyDateTime _dateLastModifiedField;
        public const string DateLastModifiedKey = "dlm";
        /// <summary>
        /// Gets or sets the date the custom object was last modified.
        /// </summary>
        public DateTimeOffset? DateLastModified { get => DateLastModifiedField.Value; set => DateLastModifiedField.Value = value; }

        /// <summary>
        /// Returns a ApiProperty object to get or set the source from which the custom object was added to the database.
        /// </summary>
        public ApiProperty<int> SystemSourceField => _systemSourceField ?? (_systemSourceField = new ApiProperty<int>(this, SystemSourceKey));
        private ApiProperty<int> _systemSourceField;
        public const string SystemSourceKey = "system_source";
        /// <summary>
        /// Gets or sets the source from which the custom object was added to the database.
        /// </summary>
        public int? SystemSource { get => SystemSourceField.Value; set => SystemSourceField.Value = value; }

        /// <summary>
        /// Returns a ApiProperty object to get or set the specific location from which the custom object was added to the database.
        /// </summary>
        public ApiPropertyString SourceLocationField => _sourceLocationField ?? (_sourceLocationField = new ApiPropertyString(this, SourceLocationKey));
        private ApiPropertyString _sourceLocationField;
        public const string SourceLocationKey = "source_location";
        /// <summary>
        /// Gets or sets the specific location from which the custom object was added to the database.
        /// </summary>
        public string SourceLocation { get => SourceLocationField.Value; set => SourceLocationField.Value = value; }

        /// <summary>
        /// Returns a ApiProperty object to get or set the custom object's IP address.
        /// </summary>
        public ApiPropertyString IpAddressField => _ipAddressField ?? (_ipAddressField = new ApiPropertyString(this, IpAddressKey));
        private ApiPropertyString _ipAddressField;
        public const string IpAddressKey = "ip_addy";
        /// <summary>
        /// Gets or sets the custom object's IP address.
        /// </summary>
        public string IpAddress { get => IpAddressField.Value; set => IpAddressField.Value = value; }

        public ApiPropertyString IpAddressDisplayField => _ipAddressDisplayField ?? (_ipAddressDisplayField = new ApiPropertyString(this, IpAddressDisplayKey));
        private ApiPropertyString _ipAddressDisplayField;
        public const string IpAddressDisplayKey = "ip_addy_display";
        public string IpAddressDisplay { get => IpAddressDisplayField.Value; set => IpAddressDisplayField.Value = value; }

        /// <summary>
        /// Returns a ApiProperty object to get or set the ID of the import batch the custom object was imported with, if any.
        /// </summary>
        public ApiProperty<int> ImportIdField => _importIdField ?? (_importIdField = new ApiProperty<int>(this, ImportIdKey));
        private ApiProperty<int> _importIdField;
        public const string ImportIdKey = "import_id";
        /// <summary>
        /// Gets or sets the ID of the import batch the custom object was imported with, if any.
        /// </summary>
        public int? ImportId { get => ImportIdField.Value; set => ImportIdField.Value = value; }

        /// <summary>
        /// Returns a ApiProperty object to get or set a flag that indicates a custom object's bulk email status.
        /// </summary>
        public ApiProperty<BulkMailStatus> BulkMailField => _bulkMailField ?? (_bulkMailField = new ApiPropertyIntEnum<BulkMailStatus>(this, BulkMailKey));
        private ApiProperty<BulkMailStatus> _bulkMailField;
        public const string BulkMailKey = "bulk_mail";
        /// <summary>
        /// Gets or sets a flag that indicates a custom object's bulk email status.
        /// </summary>
        public BulkMailStatus? BulkMail { get => BulkMailField.Value; set => BulkMailField.Value = value; }

        /// <summary>
        /// Returns a ApiProperty object to get or set a flag that indicates whether or not a custom object is opted in to receive bulk texts.
        /// </summary>
        public ApiProperty<BulkSmsStatus> BulkSmsField => _bulkSmsField ?? (_bulkSmsField = new ApiPropertyIntEnum<BulkSmsStatus>(this, BulkSmsKey));
        private ApiProperty<BulkSmsStatus> _bulkSmsField;
        public const string BulkSmsKey = "bulk_sms";
        /// <summary>
        /// Gets or sets a flag that indicates whether or not a custom object is opted in to receive bulk texts.
        /// </summary>
        public BulkSmsStatus? BulkSms { get => BulkSmsField.Value; set => BulkSmsField.Value = value; }

        /// <summary>
        /// Deprecated. Returns a ApiProperty object to get or set the tags a contact is subscribed to.
        /// </summary>
        public ApiPropertyString ListTagsField => _listTagsField ?? (_listTagsField = new ApiPropertyString(this, ListTagsKey));
        private ApiPropertyString _listTagsField;
        public const string ListTagsKey = "contact_cat";
        /// <summary>
        /// Deprecated. Gets or sets the tags a contact is subscribed to.
        /// </summary>
        public string ListTags { get => ListTagsField.Value; set => ListTagsField.Value = value; }

        /// <summary>
        /// Deprecated. Returns a ApiProperty object to get or set the sequences a contact is subscribed to.
        /// </summary>
        public ApiPropertyString ListSequencesField => _listSequencesField ?? (_listSequencesField = new ApiPropertyString(this, ListSequencesKey));
        private ApiPropertyString _listSequencesField;
        public const string ListSequencesKey = "updateSequence";
        /// <summary>
        /// Deprecated. Gets or sets the sequences a contact is subscribed to.
        /// </summary>
        public string ListSequences { get => ListSequencesField.Value; set => ListSequencesField.Value = value; }

        /// <summary>
        /// Deprecated. Returns a ApiProperty object to get or set the campaigns a contact is subscribed to.
        /// </summary>
        public ApiPropertyString ListCampaignsField => _listCampaignsField ?? (_listCampaignsField = new ApiPropertyString(this, ListCampaignsKey));
        private ApiPropertyString _listCampaignsField;
        public const string ListCampaignsKey = "updateCampaign";
        /// <summary>
        /// Deprecated. Gets or sets the campaigns a contact is subscribed to.
        /// </summary>
        public string ListCampaigns { get => ListCampaignsField.Value; set => ListCampaignsField.Value = value; }

        public ApiProperty<int> BIndexField => _bindexField ?? (_bindexField = new ApiProperty<int>(this, BIndexKey));
        private ApiProperty<int> _bindexField;
        public const string BIndexKey = "bindex";
        public int? BIndex { get => BIndexField.Value; set => BIndexField.Value = value; }
    }
}
