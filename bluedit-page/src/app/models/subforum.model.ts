import { PostPreview } from './post.preview.model'

export interface SubForum {
    id : string
    name : string
    description : string
    posts : PostPreview[]
}

export interface SubforumInfo {
    id : string
    name: string
    description : string
}