export interface Reply{
    id : string
    authorId : string
    content : string
    replies : Reply[]
    time : Date
    upvotes : number
}