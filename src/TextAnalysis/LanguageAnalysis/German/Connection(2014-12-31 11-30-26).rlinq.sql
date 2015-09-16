-- TextAnalysis.SynonymMatch
CREATE TABLE [synonym_match] (
    [id] int NOT NULL,                      -- _id
    [synonym_id] varchar(255) NULL,         -- _wordDefinition1
    [base_definition_id] varchar(255) NULL, -- WordDefinition__synonymMatches
    CONSTRAINT [pk_synonym_match] PRIMARY KEY ([id])
)

go

-- TextAnalysis.WordDefinition
CREATE TABLE [word_definition] (
    [id] varchar(255) NOT NULL,             -- _id
    [last_updated] datetime NOT NULL,       -- _lastUpdated
    [word_frequency_id] int NOT NULL,       -- _wordFrequency
    [word_typ_id] int NOT NULL,             -- _wordTyp
    CONSTRAINT [pk_word_definition] PRIMARY KEY ([id])
)

go

-- TextAnalysis.WordFrequency
CREATE TABLE [word_frequency] (
    [id] int NOT NULL,                      -- _id
    [nme] varchar(255) NULL,                -- _name
    CONSTRAINT [pk_word_frequency] PRIMARY KEY ([id])
)

go

-- TextAnalysis.WordMatch
CREATE TABLE [word_match] (
    [word] varchar(255) NOT NULL,           -- _word
    [definition_id] varchar(255) NULL,      -- _wordDefinition
    CONSTRAINT [pk_word_match] PRIMARY KEY ([word])
)

go

-- TextAnalysis.WordTyp
CREATE TABLE [word_typ] (
    [id] int NOT NULL,                      -- _id
    [nme] varchar(255) NULL,                -- _name
    CONSTRAINT [pk_word_typ] PRIMARY KEY ([id])
)

go

ALTER TABLE [synonym_match] ADD CONSTRAINT [ref_synnym_mtch_wrd_d_E1168F28] FOREIGN KEY ([synonym_id]) REFERENCES [word_definition]([id])

go

ALTER TABLE [synonym_match] ADD CONSTRAINT [ref_synnym_mtch_wrd_d_E1168F22] FOREIGN KEY ([base_definition_id]) REFERENCES [word_definition]([id])

go

ALTER TABLE [word_definition] ADD CONSTRAINT [ref_wrd_dfntn_wrd_frq_40DCAC09] FOREIGN KEY ([word_frequency_id]) REFERENCES [word_frequency]([id])

go

ALTER TABLE [word_definition] ADD CONSTRAINT [ref_word_definition_word_typ] FOREIGN KEY ([word_typ_id]) REFERENCES [word_typ]([id])

go

ALTER TABLE [word_match] ADD CONSTRAINT [ref_word_match_word_definition] FOREIGN KEY ([definition_id]) REFERENCES [word_definition]([id])

go

-- Index 'idx_synnym_mtch_bs_dfnition_id' was not detected in the database. It will be created
CREATE INDEX [idx_synnym_mtch_bs_dfnition_id] ON [synonym_match]([base_definition_id])

go

-- Index 'idx_wrd_dfntn_wrd_frequency_id' was not detected in the database. It will be created
CREATE INDEX [idx_wrd_dfntn_wrd_frequency_id] ON [word_definition]([word_frequency_id])

go

-- Index 'idx_wrd_definition_word_typ_id' was not detected in the database. It will be created
CREATE INDEX [idx_wrd_definition_word_typ_id] ON [word_definition]([word_typ_id])

go

-- Index 'idx_word_match_definition_id' was not detected in the database. It will be created
CREATE INDEX [idx_word_match_definition_id] ON [word_match]([definition_id])

go

